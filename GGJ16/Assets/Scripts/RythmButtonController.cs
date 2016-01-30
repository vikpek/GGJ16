using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Premature, Perfect, Great, Ok, Miss};

	private RythmButtonStatus _status;

	private Button _rythmButton;

	[SerializeField]
	private float _delayPremature;

	[SerializeField]
	private float _delayPerfect;

	[SerializeField]
	private float _delayGreat;

	[SerializeField]
	private float _delayOk;

	[SerializeField]
	private float _delayMiss;

	[SerializeField]
	private GameObject IndicatorRing;

	private float _timeLeft;

	void Start () {
		_rythmButton = GetComponent<Button> ();
		_status = RythmButtonStatus.Passive;	
	}

	public void ActivateRythmButton(){
		StartCoroutine (PrematureToPerfect ());
	}


	/// <summary>
	/// Change status Prematures to perfect.
	/// </summary>
	/// <returns>The to perfect.</returns>
	IEnumerator PrematureToPerfect() {

		_status = RythmButtonStatus.Premature;

		GameObject circleIndicator = (GameObject) Instantiate(IndicatorRing, transform.position, transform.rotation);
//		circleIndicator.GetComponent<CircleIndicatorController> ().IndicatorSpeed = DelayPremature;
		circleIndicator.transform.parent = transform;


		yield return new WaitForSeconds(_delayPremature);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (PerfectToGreat ());
		}
	}


	/// <summary>
	/// Status change Perfects to great.
	/// </summary>
	/// <returns>The to great.</returns>
	IEnumerator PerfectToGreat() {
		_status = RythmButtonStatus.Perfect;

		_rythmButton.image.color = Color.green;
		yield return new WaitForSeconds(_delayPerfect);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToOk ());
		}
	}

	/// <summary>
	/// Status change Greats to ok.
	/// </summary>
	/// <returns>The to ok.</returns>
	IEnumerator GreatToOk() {
		_rythmButton.image.color = Color.yellow;
		_status = RythmButtonStatus.Great;
		yield return new WaitForSeconds(_delayGreat);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (OkToMiss ());
		}

	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator OkToMiss() {
		_rythmButton.image.color = Color.red;
		_status = RythmButtonStatus.Ok;
		yield return new WaitForSeconds(_delayOk);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (MissToPassive ());
		}
	}

	/// <summary>
	/// Status change Misses to passive.
	/// </summary>
	/// <returns>The to passive.</returns>
	IEnumerator MissToPassive() {
		_status = RythmButtonStatus.Miss;
		_rythmButton.image.color = Color.grey;
		yield return new WaitForSeconds(_delayMiss);
		_status = RythmButtonStatus.Passive;
	}

	/// <summary>
	/// Tappeds the rythm button.
	/// </summary>
	public void TappedRythmButton(){
		print (_status);
		_status = RythmButtonStatus.Passive;
		_rythmButton.image.color = Color.grey;
		ScoreManager.Instance.SendScore (_status);
	}

	/// <summary>
	/// Gets the rythm status.
	/// </summary>
	/// <returns>The rythm status.</returns>
	public RythmButtonStatus GetRythmStatus()
	{
		return _status;
	}
}
