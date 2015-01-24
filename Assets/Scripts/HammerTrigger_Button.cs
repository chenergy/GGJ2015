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

	public ButtonState state = ButtonState.BUTTON_DOWN;

	private SpriteRenderer spriteRenderer;



	void Start (){
		this.spriteRenderer = this.GetComponent <SpriteRenderer> ();

		if (state == ButtonState.BUTTON_DOWN) {
			this.spriteRenderer.sprite = down;
		} else {
			this.spriteRenderer.sprite = up;
		}
	}

	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
	}
}

