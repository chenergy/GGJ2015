using UnityEngine;

using System.Collections;



public class MovingPlatform : MonoBehaviour
{
	
	
	public Vector2 moveSpeed = new Vector2 (0, 0);
	
	public float timeToMove = 1;
	
	private Transform player;
	
	
	
	private float timeCounter;
	
	private int moveDirection;

	
	void Start ()
	{
		timeCounter = 0;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		moveDirection = 1;
		
		rigidbody2D.isKinematic = true;
		
	}
	
	
	
	void Update ()
	{
		
		timeCounter += Time.deltaTime;
		
		if (timeCounter > timeToMove) {
			
			timeCounter = 0;
			
			moveDirection *= -1;
			
		}
	}

	
	void FixedUpdate ()
	{
		rigidbody2D.velocity = moveSpeed * moveDirection;
	}
	
	
	
}