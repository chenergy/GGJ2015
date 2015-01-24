using UnityEngine;
using System.Collections;

public class ButtonTarget_Button : A_ButtonTarget
{
	public Sprite up;
	public Sprite down;

	public ButtonState currentState = ButtonState.BUTTON_UP;
	public ButtonState prevState = ButtonState.BUTTON_DOWN;

	private SpriteRenderer spriteRenderer;



	void Start (){
		this.spriteRenderer = this.GetComponent <SpriteRenderer> ();

		if (this.currentState == ButtonState.BUTTON_DOWN)
			this.spriteRenderer.sprite = down;
		else
			this.spriteRenderer.sprite = up;
	}


	public override void OnButtonHit ()
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
}

