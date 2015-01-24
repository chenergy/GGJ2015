using UnityEngine;
using System.Collections;

public enum CurrentLevel {
	BOSS_BATTLE,
	GRAVITY,
	BOMB_DISARM
}


public class GameManager : MonoBehaviour {
	private static GameManager instance = null;
	public static GameManager Instance {
		get { return instance; }
	}

	public CurrentLevel level = CurrentLevel.BOSS_BATTLE;


	void Awake (){
		if (instance != null) {
			GameObject.Destroy (this.gameObject);
		} else {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
