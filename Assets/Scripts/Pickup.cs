using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public int points = 5;

	private void OnTriggerEnter2D(Collider2D other) {
		AudioManager.instance.Play(1);
		GameController gameController = FindObjectOfType<GameController>();
		gameController.ModifyScore(points);
		Destroy(gameObject);
	}
}
