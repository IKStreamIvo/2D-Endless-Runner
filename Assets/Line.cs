using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	public LineRenderer line;
	public Transform target;

	void Start () {
		line.SetPosition(0, transform.position);
	}
	
	void LateUpdate () {
		if(target == null) return;

		line.SetPosition(1, target.position + (Vector3)target.GetComponent<DistanceJoint2D>().anchor);
	}
}
