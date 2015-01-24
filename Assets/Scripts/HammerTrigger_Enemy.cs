using UnityEngine;
using System.Collections;

public class HammerTrigger_Enemy : A_HammerTrigger
{
	public Transform enemyTransform;
	public Collider2D enemyCollider;
	public Animator enemyAnimator;

	public Transform patrolTarget1;
	public Transform patrolTarget2;

	public bool startAlive = true;

	private bool isAlive = true;
	private Transform curTarget;


	void Start (){
		this.isAlive = this.startAlive;

		if (this.startAlive) {
			this.enemyCollider.isTrigger = false;
		} else {
			this.enemyCollider.isTrigger = true;
			this.Die ();
		}

		this.curTarget = this.patrolTarget1;
	}


	void Update (){
		if (this.isAlive) {
			if (this.patrolTarget1.transform.position.x > this.enemyTransform.position.x) { // right of enemy

			} else {
			}
			//this.transform.position += new Vector3 (this.patrolTarget1.transform.position.x - this.enemyTransform.position.x, 
		}
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

		this.enemyCollider.isTrigger = true;

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

		this.enemyCollider.isTrigger = false;

		this.enemyAnimator.SetBool ("isReturning", false);

		this.isAlive = true;
	}
}

