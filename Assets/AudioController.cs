using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

public AudioSource source;
public List<AudioClip> options;
	// Use this for initialization
	public void PlaySound() {
		if(source.isPlaying) return;
		var option = options[Random.Range(0, options.Count)];
		source.clip = option;
		source.Play();
	}
}
