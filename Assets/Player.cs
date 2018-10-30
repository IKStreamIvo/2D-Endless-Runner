using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Transform rope;
	public float rotateSpeed = 3f;

	private void FixedUpdate(){
		if(rope != null){
			Vector3 diff = rope.position - transform.position;
			diff.Normalize();
	
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 45), rotateSpeed * Time.deltaTime);
		}
	}
}
