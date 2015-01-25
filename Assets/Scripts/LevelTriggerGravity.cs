using UnityEngine;
using System.Collections;

public class LevelTriggerGravity : MonoBehaviour {

	float onEnter = 0;
	float onExit = 0;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("Velocity: " + other.rigidbody2D.velocity);
		if(other.tag.Equals ("Player"))
		{
			onEnter = other.rigidbody2D.velocity.y;
			if(transform.name.Contains("leverLevel_"))
			{
				transform.renderer.enabled = false
			}
			//Debug.Log ("Player is: " + player.ToString ());
			//Debug.Log ("Level Entered" + transform.name + " by object --> " + other.name);
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag.Equals ("Player"))
		{
			onExit = other.rigidbody2D.velocity.y;
			bool sameDirection = CheckSameDirection (onEnter, onExit); //Find if its a true floor level change
			PlayerControlGravity player = (PlayerControlGravity) other.gameObject.GetComponent ("PlayerControlGravity");

			if(sameDirection && !transform.name.Equals("Level4Trigger"))
			{
				//find out if up or down level, we would have to call player control floorSettings()
				//Debug.Log ("Name before switch! : " + transform.name);
				if(transform.name.Equals("leverLevel_1_1"))
				{
					Debug.Log("Lever was pressed!");
					Destroy(GameObject.Find("Destroy_1"));
				}
				else if(transform.name.Equals("Level2Trigger"))
				{
					if(onExit < 0.0)
						player.floorSettings(1);
					else
						player.floorSettings(2); // switching between floors 1 and 2
				}
				else
				{	if(onExit < 0.0)
						player.floorSettings(2);
					else
						player.floorSettings(3); // switching between floors 1 and 2 // switching between floors 2 and 3
				}
			}
			else if(transform.name.Equals("Level4Trigger"))
			{
				//Debug.Log ("Gravity Scale: " + other.rigidbody2D.gravityScale);
				float random = Random.Range(0, 100);

				//if(random == 0) random = 1;
				//if(other.rigidbody2D.gravityScale < .2f || other.rigidbody2D.gravityScale > 3) other.rigidbody2D.gravityScale = 1;

				player.FlipVertical(50, random, 1);

				//Debug.Log ("Random Number: " + random);
				//Debug.Log ("Random Multiplier: " + randomMultiplier);
			}
		}
	}

	bool CheckSameDirection(float enter, float exit)
	{
		if(enter == 0)
			return true;
		if ((enter < 0 && exit < 0) || (enter > 0 && exit > 0))
				return true;
		else
				return false;
	}
	
}
