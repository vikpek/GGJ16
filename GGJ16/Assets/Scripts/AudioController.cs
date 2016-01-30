using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
	
	[SerializeField]
	private AudioMixerGroup audioMixerMain85;

	[SerializeField]
	private AudioMixerGroup audioMixerMain90;

	[SerializeField]
	private AudioMixerGroup audioMixerMain95;

	[SerializeField]
	private AudioMixerGroup audioMixerMain100;

	[SerializeField]
	private AudioMixerGroup audioMixerMain105;

	int track;

	void Start ()
	{
		track = 0;
		InvokeRepeating ("SwitchTrack", 0f, 10f);

	}

	void SwitchTrack ()
	{
		switch (track) {
		case 0:
			audioMixerMain85.audioMixer.SetFloat ("volumeMain85", 1f);

			track++;
			break;
		case 1:
			audioMixerMain85.audioMixer.SetFloat ("volumeMain85", -80f);
			audioMixerMain85.audioMixer.SetFloat ("volumeMain90", 1f);
			track++;
			break;
		case 2:
			audioMixerMain85.audioMixer.SetFloat ("volumeMain90", -80f);
			audioMixerMain85.audioMixer.SetFloat ("volumeMain95", 1f);
			track++;
			break;
		case 3:
			audioMixerMain85.audioMixer.SetFloat ("volumeMain95", -80f);
			audioMixerMain85.audioMixer.SetFloat ("volumeMain100", 1f);
			track++;
			break;
		case 4:
			audioMixerMain85.audioMixer.SetFloat ("volumeMain100", -80f);
			audioMixerMain85.audioMixer.SetFloat ("volumeMain105", 1f);
			break;
		}
	}

	IEnumerator FadeOut( float time, AudioMixer audioMixer, string exposed )
	{
		float index = 0.0f;
		float rate = 1.0f/time;
		while( index < -80f )
		{
			index -= rate;
			audioMixer.SetFloat( exposed, index );
			yield return null;
		}

	}

}
