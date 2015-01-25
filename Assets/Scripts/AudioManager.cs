using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource source;

	public void PlayBackgroundMusic (AudioClip clip, bool loop = false){
		this.source.clip = clip;
		this.source.loop = loop;
	}

	public void PlayClipAtLocation (AudioClip clip, Vector3 position){
		AudioSource.PlayClipAtPoint (clip, position);
	}
}

