using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class CountdownController : MonoBehaviour
{
    [SerializeField]
    private Text countDownText;

	private RythmController rythmController;

	void Start(){
		rythmController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<RythmController> ();
		StartCoroutine(GetReady());
	}

	private IEnumerator GetReady()    
	{

        StartCoroutine(CountDown(3));

        yield return new WaitForSeconds(3.25f);

		rythmController.StartRythm ();
	}

    private IEnumerator CountDown(int countdown)
    {
        countDownText.text = countdown.ToString();
        countDownText.transform.localScale = Vector3.one * 3f;
        countDownText.transform.DOScale(Vector3.one, 0.33f);
        yield return new WaitForSeconds(0.66f);

        if (countdown > 1)
        {
            countdown--;
            StartCoroutine(CountDown(countdown));
        }
        else
        {
            countDownText.text = "GO!";
            countDownText.transform.localScale = Vector3.one * 3f;
            countDownText.transform.DOScale(Vector3.one, 0.33f);
            yield return new WaitForSeconds(0.66f);
            countDownText.transform.localScale = Vector3.zero;
        }
    }
}