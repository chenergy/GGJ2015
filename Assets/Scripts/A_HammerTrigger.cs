using UnityEngine;
using System.Collections;

public abstract class A_HammerTrigger : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other){
		if (other.GetComponent <Hammer> () != null) {
			this.OnHammerHit ();
		}
	}

	protected abstract void OnHammerHit ();
}

