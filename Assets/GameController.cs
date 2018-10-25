using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public float jumpForce = 10f;
	public GameObject linePrefab;
	public Rigidbody2D playerRb;
	public DistanceJoint2D playerJoint;
	public CameraMover cameraMover;
	public float maxRopeLength;
	public float climbSpeed = 2f;
	private GameObject currentLine;

	void Start () {
		
	}
	
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			GameObject newLine = Instantiate(linePrefab, position, Quaternion.identity);
			newLine.GetComponent<Line>().target = playerJoint.transform;
			playerJoint.autoConfigureDistance = true;
			playerJoint.connectedBody = newLine.GetComponent<Rigidbody2D>();

			if(currentLine == null){ //start player
				playerRb.simulated = true;
				currentLine = newLine;
			}else{
				Destroy(currentLine);
				currentLine = newLine;
			}


			playerRb.velocity = playerRb.velocity + Vector2.right * playerJoint.distance * jumpForce;
			cameraMover.targetyX = position.x;
			cameraMover.moveSpeed = playerJoint.distance;
		}
	}

	private void FixedUpdate() {
		if(playerJoint.distance > maxRopeLength){
			playerJoint.autoConfigureDistance = false;
			playerJoint.distance = Mathf.MoveTowards(playerJoint.distance, maxRopeLength, Time.deltaTime * climbSpeed);
		}
	}
}
