using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Perfect, Great, Ok, Miss};

	RythmButtonStatus status;

	Button rythmButton;

	[SerializeField]
	float delayPerfect;

	[SerializeField]
	float delayGreat;

	[SerializeField]
	float delayOk;

	[SerializeField]
	float delayMiss;

	float timeLeft;

	[SerializeField]
	GameObject IndicatorRing;

	void Start () {
		rythmButton = GetComponent<Button> ();
		status = RythmButtonStatus.Passive;	
	}

	public void ActivateRythmButton(){
		StartCoroutine (PerfectToGreat ());
	}

	IEnumerator PerfectToGreat() {
		status = RythmButtonStatus.Perfect;

		rythmButton.image.color = Color.green;
		yield return new WaitForSeconds(delayPerfect);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToOk ());
		}
	}

	IEnumerator GreatToOk() {
		rythmButton.image.color = Color.yellow;
		status = RythmButtonStatus.Great;
		yield return new WaitForSeconds(delayGreat);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (OkToMiss ());
		}

	}

	IEnumerator OkToMiss() {
		rythmButton.image.color = Color.red;
		status = RythmButtonStatus.Ok;
		yield return new WaitForSeconds(delayOk);
		if (status != RythmButtonStatus.Passive) {
			StartCoroutine (MissToPassive ());
		}
	}
	
	IEnumerator MissToPassive() {
		status = RythmButtonStatus.Miss;
		rythmButton.image.color = Color.grey;
		yield return new WaitForSeconds(delayMiss);
		status = RythmButtonStatus.Passive;
	}

	public void TappedRythmButton(){
		print (status);
		status = RythmButtonStatus.Passive;
		rythmButton.image.color = Color.grey;
	}

	public RythmButtonStatus GetRythmStatus()
	{
		return status;
	}
}
