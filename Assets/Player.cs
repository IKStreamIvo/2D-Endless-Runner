using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public LineRenderer lineRenderer;
	public DistanceJoint2D joint;
	public float force = 5f;
	private Rigidbody2D _rb;

	void Start () {
		_rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		_rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * force * Time.deltaTime);
	}

	private void LateUpdate(){
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, joint.connectedBody.position);
	}
}
