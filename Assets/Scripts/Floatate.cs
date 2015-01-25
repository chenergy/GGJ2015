using UnityEngine;
using System.Collections;

public class Floatate : MonoBehaviour
{
	public float variance = 1.0f;
	public float speed = 1.0f;
	private float timer = 0.0f;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime * this.speed;
		this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + Mathf.Sin (timer) * this.variance, this.transform.position.z);

		if (timer > 10000)
			timer = 0.0f;
	}
}

