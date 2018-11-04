using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public List<AudioClip> audioClips;
	private List<AudioSource> freeSources = new List<AudioSource>();

	private void Awake() {
		instance = this;
	} 

	public float Play(int index){
		AudioClip clip = audioClips[index];
		float length = clip.length;
		AudioSource source;
		if(freeSources.Count > 0){
			source = freeSources[0];
			freeSources.Remove(source);
		}else{
			source = gameObject.AddComponent<AudioSource>();
		}
		source.clip = clip;
		source.time = 0f;
		source.Play();
		StartCoroutine(PoolSource(length, source));
		return length;
	}

	private IEnumerator PoolSource(float duration, AudioSource source){
		yield return new WaitForSeconds(duration);
		freeSources.Add(source);
	}
}
