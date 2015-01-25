using UnityEngine;
using System.Collections;

public class PlayerControlGravity : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	//public bool jump = false;				// Condition for whether the player should jump.


	//public float moveForce = 365f;			// Amount of force added to move the player left and right.
	//public float maxSpeedX = 5f;			// The fastest the player can travel in the x axis.
	//public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	//public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	//public AudioClip[] taunts;				// Array of clips for when the player taunts.
	//public float tauntProbability = 50f;	// Chance of a taunt happening.
	//public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	//private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool groundedG = false;			// Whether or not the player is grounded.
	//private Animator anim;					// Reference to the player's animator component.TODO: Remove if not used
	private bool airJump = false;			// Will control the ability to jump in mid air
	private bool flipGravity = true;		// Will control the ability to flip gravity on the character
	private int currentLevel = 1;
	private float airJumpForce = 0f;
	private PlayerControl player;
	//private Animator anim;					// Reference to the player's animator component.

	void Awake()
	{
		// Setting up references.
		//groundCheck = GameObject.Find("GroundFloorLevel_1").transform;
		//Debug.Log (groundCheck.tag);
		//anim = GetComponent<Animator>();
		
		player = (PlayerControl) GameObject.Find ("GameCharacter").GetComponent("PlayerControl");
	}

	void Start()
	{
		airJumpForce = player.jumpForce * .8f;
	}


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		//Debug.Log ("CharacterCheckPosition: " + transform.position);
		//Debug.Log ("GroundCheckPosition: " + groundCheck.position);
		groundedG = Physics2D.Linecast(transform.position,new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 1 << LayerMask.NameToLayer("Ground"));  

		/*if(groundedG)
			Debug.Log ("Grounded is: true");
		else
			Debug.Log ("Grounded is: false");*/

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetKeyDown("up"))
		{
			if(airJump)
			{
				player.rigidbody2D.velocity = new Vector2(0f,0.5f * player.rigidbody2D.velocity.y);
				player.jump = true;	
				if(!groundedG) player.jumpForce = airJumpForce;
			}
			else if(groundedG)
				player.jump = true;
			else
				player.jump = false;
		}

		if (Input.GetKeyDown ("space")) 
			if(flipGravity)
				FlipVertical ();
	}

	void FlipVertical ()
	{
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;

		// Multiply the player's x local scale by -1.
		rigidbody2D.gravityScale *= -1;
		rigidbody2D.velocity = rigidbody2D.velocity.y * new Vector2(1f, 0.3f); //TODO:This is to help adjust the gravitational pull down when flipping so it does not 'float' as much
	}

	public void FlipVertical (float threshold, float direction, float multiplier)
	{		
		// Multiply the player's x local scale by -1.
		if(direction < threshold)
		{			
			rigidbody2D.gravityScale *= -multiplier;
			Vector3 theScale = transform.localScale;
			theScale.y *= -1;
			transform.localScale = theScale;
		}
		else
			rigidbody2D.gravityScale *= multiplier;
	}

	/**
	 * Allows us to know what functionality will be enabled per floor level
	 */
	public void floorSettings(int level)
	{
		switch (level) 
		{
			case 1:
				airJump = false;
				flipGravity = true;
			Debug.Log ("Switched to level 1");
				break;
			case 2:
				if(rigidbody2D.gravityScale < 0) FlipVertical();
				airJump = true;
				flipGravity = false;
			Debug.Log ("Switched to level 2");
				break;
			case 3:
				airJump = true;
				flipGravity = true;
			Debug.Log ("Switched to level 3");
				break;
			default:
				break;
		}		
		currentLevel = level;
	}

}
