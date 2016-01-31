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
	private GameObject circleIndicator;

	private float _timeLeft;

	[SerializeField]
	private AudioClip[] audioclipsSuccessButton;

	[SerializeField]
	private AudioClip audioclipFailButton;

	private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem _activatedVfx;

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

        circleIndicator.GetComponent<CircleIndicatorController>().InUse = true;
        circleIndicator.GetComponent<CircleIndicatorController>().IndicatorSpeed = 0.032f;
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
			StartCoroutine (PerfectToGreat ());
		}
	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator PerfectToGreat() {
		_status = RythmButtonStatus.Perfect;

		_rythmButton.image.color = Color.green;

		yield return new WaitForSeconds(_delayPerfect);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (GreatToOk ());
		}
	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator GreatToOk() {
		_status = RythmButtonStatus.Great;

		_rythmButton.image.color = Color.yellow;

		yield return new WaitForSeconds(_delayGreat/2);
		if (_status != RythmButtonStatus.Passive) {
			StartCoroutine (OkToMiss ());
		}
	}

	/// <summary>
	/// Status Oks to miss.
	/// </summary>
	/// <returns>The to miss.</returns>
	IEnumerator OkToMiss() {
		_status = RythmButtonStatus.Ok;

		_rythmButton.image.color = Color.red;

		yield return new WaitForSeconds(_delayOk/2);
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
        circleIndicator.GetComponent<CircleIndicatorController>().InUse = false;
    }

	/// <summary>
	/// Tappeds the rythm button.
	/// </summary>
	public void TappedRythmButton()
    {
		print (_status);
        if (_status != RythmButtonStatus.Miss &&
            _status != RythmButtonStatus.Passive)
        {
            //_activatedVfx.Play();
        }

		if (_status != RythmButtonStatus.Passive) {
			audioSource.PlayOneShot ((AudioClip)audioclipsSuccessButton [Random.Range (0, audioclipsSuccessButton.Length - 1)]);
			ScoreManager.Instance.SendScore (_status);
		} else {
			audioSource.PlayOneShot (audioclipFailButton);
		}
        _status = RythmButtonStatus.Passive;
		_rythmButton.image.color = Color.grey;
        circleIndicator.GetComponent<CircleIndicatorController>().InUse = false;
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
