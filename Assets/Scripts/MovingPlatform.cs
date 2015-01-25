using UnityEngine;

using System.Collections;



public class MovingPlatform : MonoBehaviour
{
	
	
	public Vector2 moveSpeed = new Vector2 (0, 0);
	
	public float timeToMove = 1;
	
	private Transform player;
	
	
	
	private float timeCounter;
	
	private int moveDirection;
	
	private bool isColliding = false;
	
	
	
	
	
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
	
	
	
	void OnCollisionStay2D (Collision2D coll)
	{
		
		if ((!isColliding) && (coll.gameObject.tag == "Player")) {
			
			isColliding = true;
			
		}
		
	}
	
	
	
	void OnCollisionExit2D (Collision2D coll)
	{
		
		if ((isColliding) && (coll.gameObject.tag == "Player")) {
			
			isColliding = false;
			
		}
		
	}

	
	void FixedUpdate ()
	{
		rigidbody2D.velocity = moveSpeed * moveDirection;
		
		if ((isColliding)) {	
			player.rigidbody2D.velocity = rigidbody2D.velocity;
		}								
		
	}
	
	
	
}