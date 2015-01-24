using UnityEngine;
using System.Collections;

public class Phase : MonoBehaviour {

	private bool canPhase = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && canPhase) {
			collider2D.isTrigger = true;
			gameObject.renderer.material.color = Color.black;
			canPhase = false;
			Invoke("unphase", 2);
			Invoke("cooldown", 6);
		}
	}

	void unphase() {
		gameObject.renderer.material.color = Color.white;
		collider2D.isTrigger = false;
	}

	void cooldown() {
		canPhase = true;
	}
}
