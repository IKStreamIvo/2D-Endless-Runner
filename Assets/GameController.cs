using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	//Editor Configs
	[Header("References")]
	public Rigidbody2D playerRb;
	public Player player;
	public DistanceJoint2D playerJoint;
	public CameraMover cameraMover;
	public GameObject linePrefab;
	public TextMeshProUGUI scoreUI;
	public Animator canvas;
	[Header("GamePlay Properties")]
	public float jumpForce = 10f;
	public float maxRopeLength;
	public float climbSpeed = 2f;
    public float fallSpeed = 2f;
	public Vector2 maxVelocity = new Vector2(10f, 100000f);
    public float slowdownSpeed = 25f;

	//Runtime
	private GameObject currentLine;
	private int score;
    private bool falling;
	private bool dead;

    //Unity Functions
    private void Start() {
		score = 0;
	}

    void Update () {
		if(dead) return;

		if(Input.GetMouseButtonUp(0)){
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 dir = (playerRb.transform.position - position).normalized;
			int layerMask = 1 << 10;
			RaycastHit2D hit = Physics2D.Raycast(playerRb.transform.position, -dir, Mathf.Infinity, layerMask);
			if (hit.collider != null){
				GameObject newLine = Instantiate(linePrefab, new Vector3(0f, 0f, -2f) + new Vector3(hit.point.x, hit.point.y), Quaternion.identity);
				newLine.GetComponent<Line>().target = playerJoint.transform;
				playerJoint.autoConfigureDistance = true;
				playerJoint.connectedBody = newLine.GetComponent<Rigidbody2D>();

				if(currentLine == null){ //start game
					canvas.SetTrigger("StartGame");
					playerRb.simulated = true;
					currentLine = newLine;
				}else{
					Destroy(currentLine);
					currentLine = newLine;
				}

				maxRopeLength = hit.point.y;
				playerRb.velocity = playerRb.velocity + Vector2.right * playerJoint.distance * jumpForce;
				cameraMover.targetyX = hit.point.x;
				//follow speed depends on how far away the point is
				cameraMover.moveSpeed = playerJoint.distance + ((hit.point.x - Camera.main.transform.position.x)/2f);
				player.rope = newLine.transform;
				falling = false;
			}
		}
	}

	private void FixedUpdate() {
		if(playerJoint.distance != maxRopeLength && !falling){
			playerJoint.autoConfigureDistance = false;
			playerJoint.distance = Mathf.MoveTowards(playerJoint.distance, maxRopeLength, Time.deltaTime * climbSpeed);
		}else if(playerJoint.distance == maxRopeLength){
			falling = true;
		}
		if(falling){
			playerJoint.autoConfigureDistance = false;
			playerJoint.distance = Mathf.MoveTowards(playerJoint.distance, playerJoint.distance + 1f, Time.deltaTime * fallSpeed);
		}

		playerRb.velocity = Vector2.Lerp(playerRb.velocity, new Vector2(Mathf.Clamp(playerRb.velocity.x, -maxVelocity.x, maxVelocity.x), playerRb.velocity.y), slowdownSpeed / (playerJoint.distance) * Time.deltaTime);
	}

	//Functions
	public void ModifyScore(int amount){
		score += amount;
		UpdateUI();
	}

	private void UpdateUI(){
		scoreUI.SetText(score.ToString());
	}

    public void Death(){
		if(dead) return;

		Destroy(currentLine);
		playerRb.constraints = RigidbodyConstraints2D.FreezePositionX;
		playerRb.AddForce(Vector3.up * 1000f);
		//player.GetComponent<Animator>().SetTrigger("Death");
		dead = true;
		canvas.SetTrigger("Fade");
    }

	public void Quit(){
		Application.Quit();
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
