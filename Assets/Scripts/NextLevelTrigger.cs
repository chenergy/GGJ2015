using UnityEngine;
using System.Collections;

public class NextLevelTrigger : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other){
		if ((other.GetComponent<PlayerControl> () != null) ||
		    (other.GetComponent<PlayerControlGravity> () != null)) {
			GameManager.Instance.GoToNextLevel ();
		}
	}
}

