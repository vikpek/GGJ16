using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int LifeDamage = 25;

	// Use this for initialization
	void Awake ()
    {
        ScoreManager.Instance.OnScoreReceived += OnScoreReceived;
		GameModel.Instance.OnLifeChanged += OnLifeChanged;
    }

    void OnDestroy()
    {
        ScoreManager.Instance.OnScoreReceived -= OnScoreReceived;
		GameModel.Instance.OnLifeChanged -= OnLifeChanged;
    }

    private void OnScoreReceived(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        if (rythmStatus == RythmButtonController.RythmButtonStatus.Miss)
        {
            GameModel.Instance.Life -= LifeDamage;
        }
    }


	private void OnLifeChanged(int oldLife, int newLife, float lifePercentage)
	{
		if (newLife <= 0) {
			SceneManager.LoadScene ("StartMenu");
		}
	}
}
