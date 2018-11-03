using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {

	public Transform followTarget;
	public float followOffset = 5f;
	public float moveSpeed = 3f;
	public float targetyX = 0;

	private void Start() {
		targetyX = followTarget.position.x;
		//followOffset = transform.position.x - followTarget.position.x / 1.5f; 	
	}

	void FixedUpdate () {
		float targetX = targetyX;
		targetX += followOffset;
		Vector3 currPos = transform.position;
		//if(currPos.x < targetX){
			currPos.x = targetX;
			transform.position = Vector3.MoveTowards(transform.position, currPos, Time.deltaTime * moveSpeed);
		//}
	}
}
