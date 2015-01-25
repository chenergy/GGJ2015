using UnityEngine;
using System.Collections;

public class HammerTrigger_Conversion : A_HammerTrigger
{
	public Sprite pastSprite;
	public Sprite presentSprite;

	public Transform targetTransform;
	public Collider2D targetCollider;

	public float duration = 1.0f;

	private SpriteRenderer spriteRenderer;
	private Vector3 startScale;


	void Start (){
		this.spriteRenderer = this.GetComponent<SpriteRenderer> ();
		this.spriteRenderer.sprite = presentSprite;

		this.startScale = this.targetTransform.localScale;
	}

	protected override void OnHammerHit ()
	{
		StartCoroutine ("TransitionToSprite");
	}


	IEnumerator TransitionToSprite (){
		float timer = 0.0f;
		float maxTime = 0.1f;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			float xScaleValue = Mathf.Lerp (this.startScale.x, 0.0f, timer / maxTime);
			float yScaleValue = Mathf.Lerp (this.startScale.y, 0.0f, timer / maxTime);

			this.targetTransform.localScale = new Vector3 (xScaleValue, yScaleValue, this.targetTransform.position.z);
		}

		timer = 0.0f;

		this.spriteRenderer.sprite = pastSprite;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			float xScaleValue = Mathf.Lerp (0.0f, this.startScale.x, timer / maxTime);
			float yScaleValue = Mathf.Lerp (0.0f, this.startScale.y, timer / maxTime);

			this.targetTransform.localScale = new Vector3 (xScaleValue, yScaleValue, this.targetTransform.position.z);
		}

		this.targetCollider.enabled = false;

		timer = 0.0f;

		// Delay
		while (timer < this.duration) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		timer = 0.0f;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			float xScaleValue = Mathf.Lerp (this.startScale.x, 0.0f, timer / maxTime);
			float yScaleValue = Mathf.Lerp (this.startScale.y, 0.0f, timer / maxTime);

			this.targetTransform.localScale = new Vector3 (xScaleValue, yScaleValue, this.targetTransform.position.z);
		}

		timer = 0.0f;

		this.spriteRenderer.sprite = presentSprite;

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			float xScaleValue = Mathf.Lerp (0.0f, this.startScale.x, timer / maxTime);
			float yScaleValue = Mathf.Lerp (0.0f, this.startScale.y, timer / maxTime);

			this.targetTransform.localScale = new Vector3 (xScaleValue, yScaleValue, this.targetTransform.position.z);
		}

		this.targetCollider.enabled = true;

		this.triggered = false;
	}
}

