using UnityEngine;
using System.Collections;


public class StepTrigger : MonoBehaviour {

	public Sprite up;
	public Sprite down;

	public ButtonState currentState = ButtonState.BUTTON_UP;
	public ButtonState prevState = ButtonState.BUTTON_DOWN;
	
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start (){
		this.spriteRenderer = this.GetComponent <SpriteRenderer> ();
		
		if (this.currentState == ButtonState.BUTTON_DOWN)
			this.spriteRenderer.sprite = down;
		else
			this.spriteRenderer.sprite = up;
	}
	
	// Update is called once per frame

	void OnHammerHit ()
	{
		if (this.currentState != this.prevState) {
			this.currentState = prevState;
			
			if (this.currentState == ButtonState.BUTTON_DOWN)
				this.spriteRenderer.sprite = down;
			else
				this.spriteRenderer.sprite = up;

			
			this.collider2D.enabled = false;
		} 
	}

	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.GetComponent<PlayerControl> () != null) {
			Debug.Log ("stepped");
		}
		
	}

}
