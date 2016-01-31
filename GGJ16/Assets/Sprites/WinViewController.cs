using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinViewController : MonoBehaviour
{
    [SerializeField]
    private CreatureView CreatureView;

    [SerializeField]
    private Transform CreaturePosition;

    [SerializeField]
    private Text FinalScore;
    
    [SerializeField]
    private Text DescriptionText;
    
    [SerializeField]
    private AnimationCurve _finalScoreScaleAnimationCurve;

    [SerializeField]
    private Text PerfectText;
    [SerializeField]
    private Text GreatText;
    [SerializeField]
    private Text OkText;
    [SerializeField]
    private Text MissText;

    public void UpdateView()
    {
        GameObject creature = CreatureView.ActiveCreature();
        creature.transform.parent = CreaturePosition;
        creature.transform.localPosition = Vector3.zero;
        creature.transform.localScale = Vector3.one * 30;

        FinalScore.text = GameModel.Instance.Score.ToString();

        FinalScore.transform.DOScale(1.25f, 2.0f).SetEase(_finalScoreScaleAnimationCurve).SetLoops(-1);

        // It's Son Goku
        if (GameModel.Instance.CreatureLevel > 8)
        {
            creature.transform.localScale = Vector3.one * 300;
            DescriptionText.text = "It's over 9.000!!!";
        }
        else
        {
            DescriptionText.text = "Thank's for summoning!";
        }

        PerfectText.text = "Perfect\n       " + GameModel.Instance.PerfectCount + "x";
        GreatText.text = "Great\n       " + GameModel.Instance.GreatCount + "x";
        OkText.text = "Ok\n       " + GameModel.Instance.OkCount + "x";
        MissText.text = "Miss\n       " + GameModel.Instance.MissCount + "x";
    }

    public void SwitchToStartScreen()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
