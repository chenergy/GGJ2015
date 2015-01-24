using UnityEngine;
using System.Collections;


public class HammerTrigger_Movable : A_HammerTrigger
{
	public float totalTime;
	public Vector2 forceVector;

	private float appliedTime;
	private bool isGoingForward;

	private SpriteRenderer spriteRenderer;

	void Start (){
		appliedTime = 0;
		isGoingForward = true;
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
