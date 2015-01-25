using UnityEngine;
using System.Collections;


public class HammerTrigger_Gate : A_HammerTrigger
{
	public GameObject gate;
	public Transform start;
	public Transform end;

	private Vector2 startPoint;
	private Vector2 endPoint;



	void Start (){
		this.startPoint = new Vector2 (this.start.position.x, this.start.position.y);
		this.endPoint = new Vector2 (this.end.position.x, this.end.position.y);
	}

	protected override void OnHammerHit ()
	{
		StartCoroutine ("MoveToEndPoint");
	}

	IEnumerator MoveToEndPoint () {
		float timer = 0.0f;
		float maxTime = 0.5f;

		if (this.target != null)
			this.target.OnButtonHit ();

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			if (this.gate != null)
				this.gate.transform.position = Vector2.Lerp (this.startPoint, this.endPoint, timer / maxTime);
		}
	}
}

