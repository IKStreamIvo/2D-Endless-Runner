using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public GameController gameController;
	public Transform rope;
	public float rotateSpeed = 3f;

	private void FixedUpdate(){
		if(rope != null){
			Vector3 diff = rope.position - transform.GetChild(0).position;
			diff.Normalize();
	
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, Quaternion.Euler(0f, 0f, rot_z - 45), rotateSpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Respawn")){
			//GameController gameController = FindObjectOfType<GameController>();
			gameController.Death();
		}
	}
}
