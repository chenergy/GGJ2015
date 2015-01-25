using UnityEngine;
using System.Collections;

public class PlayerControlGravity : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.


	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeedX = 5f;			// The fastest the player can travel in the x axis.
	public float maxSpeedY = 5f;			// The fastest the player can travel in the y axis.TODO: check if remains
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	//private Animator anim;					// Reference to the player's animator component.TODO: Remove if not used
	private bool airJump = false;			// Will control the ability to jump in mid air
	private bool flipGravity = true;		// Will control the ability to flip gravity on the character
	private int currentLevel = 1;

	void Awake()
	{
		// Setting up references.
		groundCheck = GameObject.Find("FirstFloorGround").transform;
		Debug.Log (groundCheck.tag);
		//anim = GetComponent<Animator>(); TODO: Remove if not used
	}


	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
		//Debug.Log ("CharacterCheckPosition: " + transform.position);
		//Debug.Log ("GroundCheckPosition: " + groundCheck.position);
		grounded = Physics2D.Linecast(transform.position,new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z), 1 << LayerMask.NameToLayer("Ground"));  

		/*if(grounded)
			Debug.Log ("Grounded is: true");
		else
			Debug.Log ("Grounded is: false");*/

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetKeyDown("up"))
		{
			if(airJump)
				jump = true;			
			else if(grounded)
				jump = true;
			else
				jump = false;

			/*if(jump)
			Debug.Log ("Jump is true");
			else
			Debug.Log ("Jump is false");

			if(airJump)
				Debug.Log ("airJump is true");
			else
				Debug.Log ("airJump is false");*/
			/*switch(currentLevel)
			{
			case 1:
				if(grounded) 
					jump = true;
				else
					jump = false;
				break;
			case 2:
				jump = true;
				break;
			case 3:
				jump = true;
				break;
			}*/
		}

		if (Input.GetKeyDown ("space")) 
		{
			if(flipGravity)
			{
				FlipVertical ();
			}

			/*
			switch(currentLevel)
			{
			case 1:
				rigidbody2D.gravityScale *= -1;//TODO:needs to change with levels
				FlipVertical ();
				break;
			case 2:
				break;
			case 3:
				rigidbody2D.gravityScale *= -1;//TODO:needs to change with levels
				FlipVertical ();
				break;
			}*/
		}

	}

	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached max`SpeedX yet...
		if(h * rigidbody2D.velocity.x < maxSpeedX)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeedX...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeedX)
			// ... set the player's velocity to the maxSpeedX in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeedX, rigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		// If the player should jump...
		if(jump)
		{
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");

			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			//AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce * rigidbody2D.gravityScale));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FlipVertical ()
	{
		Vector3 theScale = transform.localScale;
		theScale.y *= -1;
		transform.localScale = theScale;

		// Multiply the player's x local scale by -1.
		rigidbody2D.gravityScale *= -1;
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


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!audio.isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				audio.clip = taunts[tauntIndex];
				audio.Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
