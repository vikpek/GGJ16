using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Premature, Perfect, Great, Ok, Miss};

	private RythmButtonStatus Status;

	private Button RythmButton;

	[SerializeField]
	private float DelayPremature;

	[SerializeField]
	private float DelayPerfect;

	[SerializeField]
	private float DelayGreat;

	[SerializeField]
	private float DelayOk;

	[SerializeField]
	private float DelayMiss;

	private float TimeLeft;

	[SerializeField]
	private GameObject IndicatorRing;

	void Start () {
		RythmButton = GetComponent<Button> ();
		Status = RythmButtonStatus.Passive;	
	}

	public void ActivateRythmButton(){
		StartCoroutine (PrematureToPerfect ());
	}


	/// <summary>
	/// Change status Prematures to perfect.
	/// </summary>
	/// <returns>The to perfect.</returns>
	IEnumerator PrematureToPerfect() {

		Status = RythmButtonStatus.Premature;

		GameObject circleIndicator = (GameObject) Instantiate(IndicatorRing, transform.position, transform.rotation);
//		circleIndicator.GetComponent<CircleIndicatorController> ().IndicatorSpeed = DelayPremature;
		circleIndicator.transform.parent = transform;


		yield return new WaitForSeconds(DelayPremature);
		if (Status != RythmButtonStatus.Passive) {
			StartCoroutine (PerfectToGreat ());
		}
	}


	/// <summary>
	/// Status change Perfects to great.
	/// </summary>
	/// <returns>The to great.</returns>
	IEnumerator PerfectToGreat() {
		Status = RythmButtonStatus.Perfect;

		RythmButton.image.color = Color.green;
		yield return new WaitForSeconds(DelayPerfect);
		if (Status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToOk ());
		}
	}

	/// <summary>
	/// Status change Greats to ok.
	/// </summary>
	/// <returns>The to ok.</returns>
	IEnumerator GreatToOk() {
		RythmButton.image.color = Color.yellow;
		Status = RythmButtonStatus.Great;
		yield return new WaitForSeconds(DelayGreat);
		if (Status != RythmButtonStatus.Passive) {
			StartCoroutine (OkToMiss ());
		}

	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator OkToMiss() {
		RythmButton.image.color = Color.red;
		Status = RythmButtonStatus.Ok;
		yield return new WaitForSeconds(DelayOk);
		if (Status != RythmButtonStatus.Passive) {
			StartCoroutine (MissToPassive ());
		}
	}

	/// <summary>
	/// Status change Misses to passive.
	/// </summary>
	/// <returns>The to passive.</returns>
	IEnumerator MissToPassive() {
		Status = RythmButtonStatus.Miss;
		RythmButton.image.color = Color.grey;
		yield return new WaitForSeconds(DelayMiss);
		Status = RythmButtonStatus.Passive;
	}

	/// <summary>
	/// Tappeds the rythm button.
	/// </summary>
	public void TappedRythmButton(){
		print (Status);
		Status = RythmButtonStatus.Passive;
		RythmButton.image.color = Color.grey;
	}

	/// <summary>
	/// Gets the rythm status.
	/// </summary>
	/// <returns>The rythm status.</returns>
	public RythmButtonStatus GetRythmStatus()
	{
		return Status;
	}
}
