using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    #region Inspector
    [Header("Score Settings")]
    [SerializeField]
    private int PassiveScore = 0;

    [SerializeField]
    private int PerfectScore = 100;

    [SerializeField]
    private int GreatScore = 50;

    [SerializeField]
    private int OkScore = 20;

    [SerializeField]
    private int MissScore = 0;

    [Header("Combo Settings")]
    [SerializeField]
    private int MaxComboMultiplier = 100;

    [SerializeField]
    private float ComboScoreMultiplier = 0.1f;
    #endregion

    #region Delegate
    public delegate void ScoreReceived(RythmButtonController.RythmButtonStatus rythmStatus);
    public event ScoreReceived OnScoreReceived;
    #endregion

    #region Singleton pattern
    private static ScoreManager instance = null;

    // Game Instance Singleton
    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    /// <summary>
    /// Resets the score
    /// </summary>
    public void Reset()
    {
        GameModel.Instance.Score = 0;
        GameModel.Instance.Combo = 0;
    }

    /// <summary>
    /// Use this method to send a score to the score system
    /// </summary>
    public void SendScore(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        // Sent event
        if (OnScoreReceived != null)
        {
            OnScoreReceived(rythmStatus);
        }

        // Update combo
        if (rythmStatus != RythmButtonController.RythmButtonStatus.Miss)
        {
            GameModel.Instance.Combo++;
        }
        else
        {
            GameModel.Instance.Combo = 0;
        }

        // Update score
        GameModel.Instance.Score += GetScore(rythmStatus);
    }

    /// <summary>
    /// Return a score based on <see cref="RythmButtonController.RythmButtonStatus"/> and takes combos into account.s
    /// </summary>
    private int GetScore(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        // When the player missed a note, the combo will be reseted and miss score will be sent.
        if (rythmStatus == RythmButtonController.RythmButtonStatus.Miss)
        {
            GameModel.Instance.Combo = 0;
            return MissScore;
        }

        // Get base score
        int baseScore = 0;

        switch (rythmStatus)
        {
            case RythmButtonController.RythmButtonStatus.Passive:
                baseScore = PassiveScore;
                break;
            case RythmButtonController.RythmButtonStatus.Perfect:
                baseScore = PerfectScore;
                break;
            case RythmButtonController.RythmButtonStatus.Great:
                baseScore = GreatScore;
                break;
            case RythmButtonController.RythmButtonStatus.Ok:
                baseScore = OkScore;
                break;
            default:
                baseScore = 0;
                break;
        }

        // Calculate final score for notes
        float scoreMultiplier = Mathf.Min(GameModel.Instance.Combo, MaxComboMultiplier) * ComboScoreMultiplier;
        int finalScore = (int)(baseScore * scoreMultiplier);

        // Return score
        return finalScore;
    }
}
