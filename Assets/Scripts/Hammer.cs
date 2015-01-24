using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour
{
	public Animator hammerAnim;
	private bool isHammering = false;
	public bool IsHammering {
		get { return this.isHammering; }
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && !this.isHammering) {
			StartCoroutine ("HammerDown");
		}
	}

	IEnumerator HammerDown () {
		float timer = 0.0f;
		float maxTime = 1.0f;

		this.isHammering = true;
		this.hammerAnim.SetBool ("isHammering", true);

		while (timer < maxTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;
		}

		this.isHammering = false;
		this.hammerAnim.SetBool ("isHammering", false);
	}
}

