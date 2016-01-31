using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour {

	[SerializeField]
	GameObject popUpField;

	void Start ()
	{
		ScoreManager.Instance.OnScoreReceived += OnScoreReceived;

        popUpField.transform.localScale = Vector3.zero;
    }

	public void ShowPopUp(string text)
    {
        popUpField.gameObject.transform.localScale = Vector3.one * 0.5f;
        popUpField.GetComponent<Text>().text = text;
		PopUp ();
	}

	void PopUp()
    {
        popUpField.gameObject.transform.DOScale (1.2f, 0.5f).SetEase
            (
            new AnimationCurve(
                new Keyframe(0, 0),
                new Keyframe(0.25f, 1f),
                new Keyframe(1, 1)
                )
            ).
            OnComplete (
			() => {
                popUpField.transform.DOScale (0.01f, 0.2f);
			});
	}
		
	void OnDestroy()
	{
		ScoreManager.Instance.OnScoreReceived -= OnScoreReceived;
	}
		
	private void OnScoreReceived(RythmButtonController.RythmButtonStatus status)
	{
		ShowPopUp (status.ToString());
	}
}
