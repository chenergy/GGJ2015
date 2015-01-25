using UnityEngine;
using System.Collections;

public enum ButtonState {
	BUTTON_UP,
	BUTTON_DOWN
}

public class HammerTrigger_Button : A_HammerTrigger
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


	protected override void OnHammerHit ()
	{
		if (this.currentState != this.prevState) {
			this.currentState = prevState;

			if (this.currentState == ButtonState.BUTTON_DOWN)
				this.spriteRenderer.sprite = down;
			else
				this.spriteRenderer.sprite = up;

			if (this.target != null)
				this.target.OnButtonHit ();

			this.collider2D.enabled = false;
		} 
	}
}

