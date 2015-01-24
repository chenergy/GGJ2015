using UnityEngine;
using System.Collections;


public class HammerTrigger_Movable : A_HammerTrigger
{

	public float speed;

	public Transform startTransform;
	public Transform finalTransform;

	public Vector2 startVector;
	public Vector2 endVector; 

	private SpriteRenderer spriteRenderer;
	private bool isGoingForward;
	private Vector2 currentVector;

	void Start (){
		isGoingForward = true;
		currentVector = startVector; 
		startVector = startTransform.position;
		endVector = finalTransform.position;
	}

	void Update(){
		//this.GetComponent<Transform> ();
		if (isGoingForward &&(currentVector != endVector)) {
				StepForward ();

		} else {
		}
	}
	
	void StepForward(){
		Transform thisTransform = this.GetComponent<Transform> ();
		Vector3 direction = endVector-startVector;
		thisTransform.position += direction.normalized*speed*Time.deltaTime;
	}


	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
		isGoingForward = false;
	}
}
