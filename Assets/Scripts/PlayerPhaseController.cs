using UnityEngine;
using System.Collections;

public class PlayerPhaseController : MonoBehaviour {
	
	private bool canPhase = true;
	
	public GameObject phaseAnimation;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		if (Input.GetKeyDown(KeyCode.Space) && canPhase) {
			canPhase = false;
			transform.FindChild ("body").renderer.material.color = Color.black;
			if( this.phaseAnimation != null ) {
				Instantiate(this.phaseAnimation, transform.position, Quaternion.identity);
			}
			Invoke ("endPhase", 1);
			Invoke("cooldownPhase", 2);
		}
	}
	// Update is called once per frame
	void Update () {

	}

void endPhase() {
	transform.FindChild("body").renderer.material.color = Color.red;
}

void cooldownPhase() {
	transform.FindChild("body").renderer.material.color = Color.white;
	canPhase = true;
}
}