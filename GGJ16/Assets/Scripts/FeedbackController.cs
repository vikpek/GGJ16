using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour {

	[SerializeField]
	GameObject popUpField;

	GameObject popUpFieldInstance;


	void Start ()
	{
		ScoreManager.Instance.OnScoreReceived += OnScoreReceived;
	}

	public void ShowPopUp(string text){
		popUpFieldInstance = (GameObject) Instantiate (popUpField, transform.position, Quaternion.identity);
		popUpFieldInstance.gameObject.transform.parent = transform;

        RectTransform popuptransform = (RectTransform) popUpFieldInstance.gameObject.transform;
        popuptransform.offsetMin = Vector2.zero;
        popuptransform.offsetMax = Vector2.zero;

        popUpFieldInstance.gameObject.transform.localScale = Vector3.one * 0.5f;
        popUpFieldInstance.GetComponent<Text>().text = text;
		PopUp ();
	}

	void PopUp()
    {
        popUpFieldInstance.gameObject.transform.DOScale (1.2f, 0.5f).SetEase
            (
            new AnimationCurve(
                new Keyframe(0, 0),
                new Keyframe(0.25f, 1f),
                new Keyframe(1, 1)
                )
            ).
            OnComplete (
			() => {
				popUpFieldInstance.transform.DOScale (0.01f, 0.2f);
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
