using UnityEngine;
using System.Collections;


public class HammerTrigger_Movable : A_HammerTrigger
{

	public float speed;
	public Transform beginTransform;
	public Transform finalTransform;
	public bool startAtEnd;
	public float maxTime;

	public Vector2 startVector;
	public Vector2 endVector; 


	private SpriteRenderer spriteRenderer;
	private bool isGoingForward;
	private Vector2 currentVector;
	private float resetTime;

	void Start (){
		isGoingForward = true;
		resetTime = 0;
		startVector = beginTransform.position;
		endVector = finalTransform.position;
		if (!startAtEnd) {
			this.GetComponent<Transform> ().position = startVector;
			currentVector = startVector; 
		} else {
			this.GetComponent<Transform> ().position = endVector;
			currentVector = endVector;
		}
	}

	void Update(){
		//this.GetComponent<Transform> ();
		if (isGoingForward && (currentVector != endVector)) {
			StepForward ();
		} else if (!isGoingForward && (currentVector != startVector)) {
			StepBackward ();
		} else if (!isGoingForward && (maxTime > 0)) {
			StepResetTime();
		}
	}
	
	void StepForward(){
		Transform thisTransform = this.GetComponent<Transform> ();
		Vector3 direction = endVector-startVector;
		thisTransform.position += direction.normalized*speed*Time.deltaTime;
		currentVector = thisTransform.position;
		if (Vector3.Dot (endVector - currentVector, direction) <= 0) {
			thisTransform.position = endVector;
			currentVector = endVector;
		}
	}

	void StepBackward(){
		Transform thisTransform = this.GetComponent<Transform> ();
		Vector3 direction = startVector-endVector;
		thisTransform.position += direction.normalized*speed*Time.deltaTime;
		currentVector = thisTransform.position;
		if (Vector3.Dot (startVector - currentVector, direction) <= 0) {
			thisTransform.position = startVector;
			currentVector = startVector;
		}
	}

	void StepResetTime(){
		resetTime += Time.deltaTime;
		if (resetTime >= maxTime) {
			resetTime = 0;
			isGoingForward = true;
			this.triggered = false;
		}
	}

	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
		isGoingForward = false;
	}
}
