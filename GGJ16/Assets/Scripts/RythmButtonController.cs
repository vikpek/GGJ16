using UnityEngine;
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

	private GameObject circleIndicator;

	[SerializeField]
	private AudioClip[] buttonSounds;

	private AudioSource audioSource;

	void Start () {
		_rythmButton = GetComponent<Button> ();
		_status = RythmButtonStatus.Passive;	
		audioSource = GetComponent<AudioSource> ();
	}

	public void ActivateRythmButton(){
		StartCoroutine (OkToGreat ());
	}

	/// <summary>
	/// Status change Perfects to great.
	/// </summary>
	/// <returns>The to great.</returns>
	IEnumerator OkToGreat() {
		_status = RythmButtonStatus.Ok;

		circleIndicator = (GameObject) Instantiate(IndicatorRing, transform.position, Quaternion.identity);
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
		_status = RythmButtonStatus.Great;

		_rythmButton.image.color = Color.yellow;

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
		_status = RythmButtonStatus.Perfect;

		_rythmButton.image.color = Color.green;

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

		ScoreManager.Instance.SendScore(RythmButtonStatus.Miss);
		_status = RythmButtonStatus.Passive;
		_rythmButton.image.color = Color.grey;
		Destroy (circleIndicator);
	}

	/// <summary>
	/// Tappeds the rythm button.
	/// </summary>
	public void TappedRythmButton(){
		print (_status);
		if (_status == RythmButtonStatus.Passive) {
			ScoreManager.Instance.SendScore (RythmButtonStatus.Miss);
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<ScreenshakeController> ().ScreenShake(1f, 0.4f);
		} else {
			audioSource.clip = (AudioClip) buttonSounds [Random.Range (0, buttonSounds.Length - 1)];
			audioSource.Play ();
			ScoreManager.Instance.SendScore (_status);
		}
        _status = RythmButtonStatus.Passive;
		_rythmButton.image.color = Color.grey;
		Destroy (circleIndicator);
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
