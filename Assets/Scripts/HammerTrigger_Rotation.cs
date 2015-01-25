using UnityEngine;
using System.Collections;

public class HammerTrigger_Rotation : A_HammerTrigger
{
	public float speed;
	public float beginAngle;
	public float finalAngle;
	private bool isGoingForward;
	public int numRotations;

	public int rotationSign;
	private SpriteRenderer spriteRenderer;
	private int currentRotation;
	private float correctedFinalAngle;
	private float correctedCurrentAngle;
	// Use this for initialization
	void Start ()
	{
		isGoingForward = true;
		currentRotation = 0;
		correctedFinalAngle = numRotations * 360 + finalAngle;
		correctedCurrentAngle = this.transform.rotation.eulerAngles.z;
		if (beginAngle > 180) {
			beginAngle -= 360;
		} 
//		this.transform.Rotate(new Vector3(0, 0, finalAngle));

	}
	
	// Update is called once per frame
	void Update (){
		float currAngle = this.transform.rotation.eulerAngles.z;
		currAngle = currAngle % 360;
		if (currAngle > 180) {
			currAngle -= 360;
		}
		if (isGoingForward && (rotationSign*(finalAngle - currAngle) < 0)){
			StepForward ();
		} else if (!isGoingForward && (rotationSign*(currAngle-beginAngle) < 0)) {
			StepBackward ();		
		}
	}

	void StepForward(){
		this.transform.Rotate (new Vector3 (0, 0, -rotationSign*speed * Time.deltaTime));
		float currAngle =  this.transform.rotation.eulerAngles.z; 
		currAngle = currAngle % 360;
		if (currAngle > 180) {
			currAngle -= 360;
		}
		if (-rotationSign*currAngle > -rotationSign*finalAngle) {
			this.transform.Rotate (new Vector3(0, 0, -rotationSign*(currAngle-finalAngle)));
		}
	}
	
	void StepBackward(){
		this.transform.Rotate (new Vector3 (0, 0, rotationSign*speed * Time.deltaTime));
		float currAngle =  this.transform.rotation.eulerAngles.z; 
		currAngle = currAngle % 360;
		if (currAngle > 180) {
			currAngle -= 360;
		}
		if (rotationSign*currAngle > rotationSign*beginAngle) {
			this.transform.Rotate (new Vector3(0, 0, rotationSign*(currAngle-beginAngle)));
		}
	}


	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
		isGoingForward = false;
	}

}

