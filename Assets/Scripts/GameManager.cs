using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum CurrentLevel {
	BOSS_LEVEL,
	GRAVITY,
	BOMB_DISARM
}


public class GameManager : MonoBehaviour {
	private static GameManager instance = null;
	public static GameManager Instance {
		get { return instance; }
	}

	public Image blackScreen;
	public float blackoutTime = 1.0f;


	private int level = 0;

	private CurrentLevel[] levelList = new CurrentLevel [3] {
		CurrentLevel.BOSS_LEVEL,
		CurrentLevel.GRAVITY,
		CurrentLevel.BOMB_DISARM
	};


	void Awake (){
		if (instance != null) {
			GameObject.Destroy (this.gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (this.gameObject);
		}
	}


	public void GoToNextLevel (){
		if (instance.level < this.levelList.Length - 1) {
			instance.level++;
			Application.LoadLevel (instance.levelList [instance.level].ToString ());
		}
	}


	public void GoToPrevLevel (){
		if (instance.level > 0) {
			instance.level--;
			Application.LoadLevel (instance.levelList [instance.level].ToString ());
		}
	}

	public void FadeOut (){
		StartCoroutine ("FadeOutRoutine");
	}

	IEnumerator FadeOutRoutine() {
		float timer = 0.0f;

		while (timer < this.blackoutTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			this.blackScreen.color = new Color (this.blackScreen.color.r, this.blackScreen.color.g, this.blackScreen.color.b, timer / this.blackoutTime);
		}
	}

	public void FadeIn (){
		StartCoroutine ("FadeInRoutine");
	}

	IEnumerator FadeInRoutine() {
		float timer = 0.0f;

		while (timer < this.blackoutTime) {
			yield return new WaitForEndOfFrame ();
			timer += Time.deltaTime;

			this.blackScreen.color = new Color (this.blackScreen.color.r, this.blackScreen.color.g, this.blackScreen.color.b, 1.0f - timer / this.blackoutTime);
		}
	}
}
