using UnityEngine;
using System.Collections;

public class PlayerPhaseController : MonoBehaviour {
	
	private bool canPhase = true;
	
	public GameObject phaseAnimation;

	public AudioClip phaseBeginClip;
	public AudioClip phaseReadyClip;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		if (Input.GetKeyDown(KeyCode.Space) && canPhase) {
			canPhase = false;
			gameObject.renderer.material.color = Color.black;
			// GameManager.Instance.PlayClipAtLocation(phaseBeginClip, gameObject.transform.position);
			audio.PlayOneShot(phaseBeginClip, 4.0f);
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
	gameObject.renderer.material.color = Color.gray;
}

	void cooldownPhase() {
		//GameManager.Instance.PlayClipAtLocation(phaseReadyClip, gameObject.transform.position);
		audio.PlayOneShot(phaseReadyClip, 3.4f);
		gameObject.renderer.material.color = Color.white;
		canPhase = true;
	}
}