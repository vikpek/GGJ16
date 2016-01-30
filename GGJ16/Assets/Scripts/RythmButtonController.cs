﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Perfect, Great, Ok, Miss};

	private RythmButtonStatus _status;

	private Button _rythmButton;

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
		StartCoroutine (OkToGreat ());
	}

	/// <summary>
	/// Status change Perfects to great.
	/// </summary>
	/// <returns>The to great.</returns>
	IEnumerator OkToGreat() {
		_status = RythmButtonStatus.Perfect;

		GameObject circleIndicator = (GameObject) Instantiate(IndicatorRing, transform.position, Quaternion.identity);
		circleIndicator.GetComponent<CircleIndicatorController> ().IndicatorSpeed = (_delayPerfect + _delayGreat + _delayOk) * 0.0004f;
		circleIndicator.transform.parent = transform.parent;

		_rythmButton.image.color = Color.red;
		yield return new WaitForSeconds(_delayOk);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToPerfect ());
		}
	}

	/// <summary>
	/// Status change Greats to ok.
	/// </summary>
	/// <returns>The to ok.</returns>
	IEnumerator GreatToPerfect() {
		_rythmButton.image.color = Color.yellow;
		_status = RythmButtonStatus.Great;
		yield return new WaitForSeconds(_delayGreat);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (PerfectToMiss ());
		}
	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator PerfectToMiss() {
		_rythmButton.image.color = Color.green;
		_status = RythmButtonStatus.Ok;
		yield return new WaitForSeconds(_delayPerfect);
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
