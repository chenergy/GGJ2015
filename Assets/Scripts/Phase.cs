using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Phase : MonoBehaviour {

	private bool canPhase = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && canPhase) {
			collider2D.isTrigger = true;
			foreach(Transform child in transform) {
				child.collider2D.isTrigger = true;
			}

			gameObject.renderer.material.color = new Color(1f,1f,1f,.5f);
			canPhase = false;
			Invoke("unphase", 1);
			Invoke("cooldown", 2);
		}
	}

	void unphase() {
		gameObject.renderer.material.color = Color.white;
		collider2D.isTrigger = false;
		foreach(Transform child in transform) {
			child.collider2D.isTrigger = false;
		}
	}

	void cooldown() {
		canPhase = true;
	}
}
