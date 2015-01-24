using UnityEngine;
using System.Collections;

public class HammerTrigger_Enemy : A_HammerTrigger
{
	public Transform enemyTransform;
	public Collider2D enemyCollider;
	public Animator enemyAnimator;

	public bool startAlive = true;

	private bool isAlive = true;


	void Start (){
		this.isAlive = this.startAlive;

		if (this.startAlive) {
			this.enemyCollider.isTrigger = false;
		} else {
			this.enemyCollider.isTrigger = true;
			this.Die ();
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

