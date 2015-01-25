using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	
	
	public HammerTrigger_Enemy target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.GetComponent<PlayerControl> () != null) {
			target.enabled = true;
		}
		
	}
}

