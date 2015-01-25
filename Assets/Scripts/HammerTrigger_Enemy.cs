using UnityEngine;
using System.Collections;

public class HammerTrigger_Enemy : A_HammerTrigger
{
	public Transform enemyTransform;
	public Collider2D enemyCollider;
	public Animator enemyAnimator;

	public Transform patrolTarget_L;
	public Transform patrolTarget_R;

	public bool startAlive = true;

	public float speed = 1.0f;

	private bool isAlive = true;
	private Transform curTarget;




	void Start (){
		this.isAlive = this.startAlive;

		if (this.startAlive) {

		} else {
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
		base.OnHammerHit ();

		this.ReturnToLife ();
	}



	public void ReturnToLife (){
		StartCoroutine ("ReturnRoutine");
	}


	public void Die (){
		StartCoroutine ("DieRoutine");
	}


	IEnumerator DieRoutine (){
		float timer = 0.0f;
		float maxTimer = 0.5f;

		this.enemyAnimator.SetBool ("isDying", true);

		while (timer < maxTimer) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}


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


		this.enemyAnimator.SetBool ("isReturning", false);

		this.isAlive = true;
	}
}

