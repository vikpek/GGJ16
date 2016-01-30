using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	[SerializeField]
	private AudioMixerGroup audioMixerGroup01;

	[SerializeField]
	private AudioMixerGroup audioMixerGroup02;

	bool dir;

	void Start(){
		dir = false;
		InvokeRepeating ("SwitchTrack", 0f, 5f);

	}

	void SwitchTrack(){
		if (dir) {
			print (audioMixerGroup01.audioMixer.SetFloat ("volume01", 1f));
			print (audioMixerGroup02.audioMixer.SetFloat ("volume02", -80f));
			dir = false;
		} else {
			print (audioMixerGroup01.audioMixer.SetFloat ("volume01", -80f));
			print (audioMixerGroup02.audioMixer.SetFloat ("volume02", 1f));
			dir = true;
		}
	}
}
