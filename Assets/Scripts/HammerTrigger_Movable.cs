using UnityEngine;
using System.Collections;

public enum ButtonState {
	BUTTON_UP,
	BUTTON_DOWN
}

public class HammerTrigger_Movable : A_HammerTrigger
{
	public float totalTime;
	private float appliedTime;
	public Vector2 forceVector;
	public bool isGoingForward;

	private SpriteRenderer spriteRenderer;

	void Start (){
		appliedTime = 0;
	}

	void Update(){
		if (isGoingForward) {
			if (appliedTime < totalTime) {
				this.rigidbody2D.AddForce (forceVector);
				appliedTime += Time.deltaTime;
			}	
		} else {
			if (appliedTime > 0) {
				this.rigidbody2D.AddForce(-forceVector);
				appliedTime -= Time.deltaTime;
			}	
		}
	}
	
	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
		isGoingForward = false;
	}
}
