using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Perfect, Great, Ok, Miss};

	RythmButtonStatus status;

	Button b;

	[SerializeField]
	float delayPerfect;

	[SerializeField]
	float delayGreat;

	[SerializeField]
	float delayOk;

	[SerializeField]
	float delayMiss;

	float timeLeft;

	void Start () {
		b = GetComponent<Button> ();
		status = RythmButtonStatus.Passive;	
		ActivateRythmButton ();

	}

	public void ActivateRythmButton(){
		StartCoroutine (PerfectToGreat ());
	}

	IEnumerator PerfectToGreat() {
		status = RythmButtonStatus.Perfect;

		b.image.color = Color.green;
		yield return new WaitForSeconds(delayPerfect);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToOk ());
		}
	}

	IEnumerator GreatToOk() {
		b.image.color = Color.yellow;
		status = RythmButtonStatus.Great;
		yield return new WaitForSeconds(delayGreat);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (OkToMiss ());
		}

	}

	IEnumerator OkToMiss() {
		b.image.color = Color.red;
		status = RythmButtonStatus.Ok;
		yield return new WaitForSeconds(delayOk);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (MissToPassive ());
		}
	}
	
	IEnumerator MissToPassive() {
		status = RythmButtonStatus.Miss;
		b.image.color = Color.grey;
		yield return new WaitForSeconds(delayMiss);
		status = RythmButtonStatus.Passive;
	}

	public void TappedRythmButton(){
		print (status);
		status = RythmButtonStatus.Passive;
		b.image.color = Color.grey;
	}
}
