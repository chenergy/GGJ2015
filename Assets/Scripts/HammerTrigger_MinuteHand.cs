using UnityEngine;
using System.Collections;

public class HammerTrigger_MinuteHand : A_HammerTrigger
{
	public float speed;
	public float angleToTraverse;

	private float angle;
	private bool isGoingForward;


	// Use this for initialization
	void Start ()
	{
		isGoingForward = true;
		angle = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isGoingForward && angle <= angleToTraverse) {
			this.transform.Rotate (new Vector3 (0, 0, speed * Time.deltaTime));
			angle += speed * Time.deltaTime;
		}
	}

	protected override void OnHammerHit ()
	{
		Debug.Log ("Hammer has hit button");
		isGoingForward = false;
	}

}

