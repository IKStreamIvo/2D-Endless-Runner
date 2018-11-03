using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	public LineRenderer line;
	public Transform target;

	void Start () {
		line.SetPosition(0, transform.position + new Vector3(0f, 0f, -1f));
	}
	
	void LateUpdate () {
		if(target == null) return;
		Vector3 pos = target.position + (Vector3)target.GetComponent<DistanceJoint2D>().anchor;
		pos.z = -3f;
		line.SetPosition(1, pos);
	}
}
