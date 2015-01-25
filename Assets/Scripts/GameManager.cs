using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum CurrentLevel {
	START,
	HAMMER,
	GRAVITY,
	BOMB_DISARM,
	CUTSCENE_0,
	CUTSCENE_1,
	CUTSCENE_2,
	END
}


/*[System.Serializable]
public class LevelBGM {
	public AudioClip START;
	public AudioClip GRAVITY;
	public AudioClip HAMMER;
	public AudioClip BOMB_DISARM;
	public AudioClip CUTSCENE_0;
	public AudioClip CUTSCENE_1;
	public AudioClip CUTSCENE_2;
	public AudioClip FINAL;
}*/



public class GameManager : MonoBehaviour {
	private static GameManager instance = null;
	public static GameManager Instance {
		get { return instance; }
	}

	public Image blackScreen;
	public float blackoutTime = 1.0f;

	public AudioManager audio;
	//public LevelBGM levelBGM;

	private int level = 0;

	private CurrentLevel[] levelList = new CurrentLevel [5] {
		CurrentLevel.CUTSCENE_0,
		CurrentLevel.BOMB_DISARM,
		CurrentLevel.GRAVITY,
		CurrentLevel.HAMMER,
		CurrentLevel.END
	};

	//private Dictionary <string, AudioClip> levelBGMDict = new Dictionary<string, AudioClip> ();


	void Awake (){
		if (instance != null) {
			GameObject.Destroy (this.gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (this.gameObject);

			/*this.levelBGMDict ["START"] = this.levelBGM.START;
			this.levelBGMDict ["GRAVITY"] = this.levelBGM.GRAVITY;
			this.levelBGMDict ["BOMB_DISARM"] = this.levelBGM.BOMB_DISARM;
			this.levelBGMDict ["CUTSCENE_0"] = this.levelBGM.CUTSCENE_0;
			this.levelBGMDict ["CUTSCENE_1"] = this.levelBGM.CUTSCENE_1;
			this.levelBGMDict ["CUTSCENE_2"] = this.levelBGM.CUTSCENE_2;
			this.levelBGMDict ["FINAL"] = this.levelBGM.FINAL;*/
		}
	}


	public void GoToNextLevel (){
		if (instance.level < this.levelList.Length - 1) {
			instance.level++;
			string newLevel = instance.levelList [instance.level].ToString ();

			/*if (this.levelBGMDict.ContainsKey (newLevel)) {
				if (this.levelBGMDict[newLevel] != null)
					this.audio.PlayBackgroundMusic (this.levelBGMDict [newLevel], true);
			}*/

			StartCoroutine (instance.GoToNextLevelRoutine (newLevel));
		}
	}


	public void GoToPrevLevel (){
		if (instance.level > 0) {
			instance.level--;
			string newLevel = instance.levelList [instance.level].ToString ();

			/*if (this.levelBGMDict.ContainsKey (newLevel)) {
				if (this.levelBGMDict[newLevel] != null)
					this.audio.PlayBackgroundMusic (this.levelBGMDict [newLevel], true);
			}*/

			Application.LoadLevel (newLevel);
		}
	}



	IEnumerator GoToNextLevelRoutine (string newLevel){
		float timer = 0.0f;
		float spinTime = 1.0f;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		if (player.GetComponent <PlayerControl> () != null)
			player.GetComponent <PlayerControl> ().enabled = false;

		if (player.GetComponent <PlayerControlGravity> () != null)
			player.GetComponent <PlayerControlGravity> ().enabled = false;

		if (player.GetComponent <Animator> () != null)
			player.GetComponent <Animator> ().enabled = false;

		player.collider2D.enabled = false;
		player.rigidbody2D.isKinematic = true;

		this.FadeOut ();

		if (player != null) {
			while (timer < spinTime) {
				yield return new WaitForEndOfFrame ();
				timer += Time.deltaTime;

				player.transform.Rotate (new Vector3 (0, 50 * timer, 0));
			}
		}

		Application.LoadLevel (newLevel);

		this.FadeIn ();
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

	public void PlayBackgroundMusic (AudioClip clip, bool loop = false){
		this.audio.PlayBackgroundMusic (clip, loop);
	}

	public void PlayClipAtLocation (AudioClip clip, Vector3 position){
		this.audio.PlayClipAtLocation (clip, position);
	}
}
