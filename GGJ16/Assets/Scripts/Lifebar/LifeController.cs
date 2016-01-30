using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int LifeDamage = 10;

	// Use this for initialization
	void Awake ()
    {
        ScoreManager.Instance.OnScoreReceived += OnScoreReceived;
    }

    void OnDestroy()
    {
        ScoreManager.Instance.OnScoreReceived -= OnScoreReceived;
    }

    private void OnScoreReceived(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        if (rythmStatus == RythmButtonController.RythmButtonStatus.Miss)
        {
            GameModel.Instance.Life -= LifeDamage;
        }
    }
}
