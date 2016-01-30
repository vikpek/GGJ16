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
		popUpFieldInstance.GetComponent<Text>().text = text;
		PopUp ();
	}

	void PopUp(){
		transform.DOScale (transform.localScale * 1.1f, 1.6f).OnComplete (
			() => {
				popUpFieldInstance.transform.DOScale (transform.localScale * 0.01f, 0.2f);
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
