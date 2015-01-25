using UnityEngine;
using System.Collections;
using System.Text;

public class CutsceneText : MonoBehaviour
{
	public float delay = 0.01f;
	public string targetString = "";


	private string curString = "";
	private StringBuilder builder;
	private int curIndex = 0;
	private float timer = 0.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > this.delay) {
		}
	}
}

