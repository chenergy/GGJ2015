using UnityEngine;
using System.Collections;

public class HammerTrigger_Enemy : A_HammerTrigger
{
	public Transform enemyTransform;
	public Collider2D enemyCollider;
	public Animator enemyAnimator;

	public Transform patrolTarget_L;
	public Transform patrolTarget_R;

	public GameObject explosion;

	public bool startAlive = true;

	public float speed = 1.0f;

	private bool isAlive = true;
	private Transform curTarget;




	void Start (){
		this.isAlive = this.startAlive;

		if (this.startAlive) {
			//this.enemyCollider.isTrigger = false;
		} else {
			//this.enemyCollider.isTrigger = true;
			this.Die ();
		}

		this.curTarget = this.patrolTarget_L;
	}


	void Update (){
		//this.enemyTransform.position += new Vector3 (1.0f, 0.0f, 0.0f);
		if (this.isAlive) {
			if (this.curTarget == this.patrolTarget_L) {
				if (this.enemyTransform.position.x < this.patrolTarget_L.position.x) {
					this.enemyTransform.position = this.patrolTarget_L.position;

					StartCoroutine ("ChangeTargetRoutine", this.patrolTarget_R);
				} else if (this.enemyTransform.position.x > this.patrolTarget_L.position.x) {
					this.enemyTransform.position += Vector3.left * speed;
					this.enemyAnimator.SetBool ("isMoving", true);
				}
			} else if (this.curTarget == this.patrolTarget_R) {
				if (this.enemyTransform.position.x > this.patrolTarget_R.position.x) {
					this.enemyTransform.position = this.patrolTarget_R.position;

					StartCoroutine ("ChangeTargetRoutine", this.patrolTarget_L);
				} else if (this.enemyTransform.position.x < this.patrolTarget_R.position.x) {
					this.enemyTransform.position += Vector3.right * speed;
					this.enemyAnimator.SetBool ("isMoving", true);
				}
			}
		}
	}


	IEnumerator ChangeTargetRoutine (Transform newTarget){
		float timer = 0.0f;
		float waitTime = 1.0f;

		this.enemyAnimator.SetBool ("isMoving", false);

		while (timer < waitTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		this.ChangeTarget (newTarget);

		this.enemyAnimator.SetBool ("isMoving", true);
	}


	private void ChangeTarget (Transform newTarget){
		this.curTarget = newTarget;

		if (this.curTarget.transform.position.x > this.enemyTransform.position.x) { // right of enemy
			this.enemyTransform.localScale = new Vector3 (-5, 5, 1);
		} else {
			this.enemyTransform.localScale = new Vector3 (5, 5, 1);
		}
		//this.transform.position += new Vector3 (this.patrolTarget1.transform.position.x - this.enemyTransform.position.x, 
	}


	protected override void OnHammerHit ()
	{
		if (!this.isAlive) {
			this.ReturnToLife ();
		} else {
			if (!this.triggered) {
				StartCoroutine ("Knockback");
			}
		}

		base.OnHammerHit ();
	}



	public void ReturnToLife (){
		StartCoroutine ("ReturnRoutine");
	}


	public void Die (){
		StartCoroutine ("DieRoutine");
	}


	IEnumerator Knockback (){
		float timer = 0.0f;
		float maxTime = 1.0f;

		this.triggered = true;
		this.isAlive = false;

		Vector3 targetDirection = Vector3.left;
		if (this.curTarget == this.patrolTarget_L)
			targetDirection = Vector3.right;
		else if (this.curTarget == this.patrolTarget_R)
			targetDirection = Vector3.left;

		//this.enemyAnimator.enabled = false;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			this.enemyTransform.position += targetDirection * this.speed;
		}

		this.isAlive = true;
		//this.enemyAnimator.enabled = true;

		this.triggered = false;
	}



	IEnumerator DieRoutine (){
		float timer = 0.0f;
		float maxTimer = 0.5f;

		this.enemyAnimator.SetBool ("isDying", true);

		while (timer < maxTimer) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		//this.enemyCollider.isTrigger = true;

		this.enemyAnimator.SetBool ("isDying", false);

		this.isAlive = false;
	}


	IEnumerator ReturnRoutine (){
		float timer = 0.0f;
		float maxTimer = 0.5f;

		this.enemyAnimator.SetBool ("isReturning", true);

		while (timer < maxTimer) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		//this.enemyCollider.isTrigger = false;

		this.enemyAnimator.SetBool ("isReturning", false);

		this.isAlive = true;

		this.triggered = false;
	}

	void OnCollisionEnter2D (Collision2D other){
		PlayerControl player = other.gameObject.GetComponent <PlayerControl> ();

		if (player != null) {
			player.gameObject.SetActive (false);

			if (this.explosion != null) {
				GameObject newExplosion = GameObject.Instantiate (this.explosion, player.transform.position, Quaternion.identity) as GameObject;

				GameObject.Destroy (newExplosion, 0.27f);

				StartCoroutine ("ResetLevel", 1.0f);
			}
		}
	}

	IEnumerator ResetLevel (float delay){
		float timer = 0.0f;

		while (timer < delay) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		Application.LoadLevel ("HAMMER");
	}
}

