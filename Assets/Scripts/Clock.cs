using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour
{
	public Transform hourHand;
	public Transform minuteHand;

	public float speed = 10.0f;


	void Start () {
		GameObject.Destroy (this.gameObject, 1.0f);
	}


	// Update is called once per frame
	void Update ()
	{
		this.minuteHand.transform.Rotate (new Vector3 (0, 0, 1) * this.speed);
		this.hourHand.transform.Rotate (new Vector3 (0, 0, 1) * this.speed * (1.0f / 60.0f));
	}
}

