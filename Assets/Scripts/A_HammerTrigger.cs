using UnityEngine;
using System.Collections;

public abstract class A_HammerTrigger : MonoBehaviour
{
	public A_ButtonTarget target;

	public GameObject clockPrefab;

	protected bool triggered = false;

	void OnTriggerStay2D (Collider2D other){
		Hammer hammer = other.GetComponent <Hammer> ();
		if (hammer != null) {
			if (hammer.IsHammering && !this.triggered) {
				this.triggered = true;

				if (this.clockPrefab != null) {
					GameObject newClock = GameObject.Instantiate (this.clockPrefab, this.transform.position + Vector3.up * 2, Quaternion.identity) as GameObject;
					newClock.transform.parent = this.transform;
				}

				this.OnHammerHit ();
			}
		}
	}

	protected abstract void OnHammerHit ();
}

