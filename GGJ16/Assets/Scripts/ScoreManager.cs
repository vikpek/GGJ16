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

    #region Properties
    private int _score;
    public int Score { get { return _score; } }

    private int _combo;
    public int Combo { get { return _combo; } }
    #endregion

    #region Delegate
    public delegate void ScoreChanged(int oldScore, int newScore);
    public event ScoreChanged OnScoreChanged;

    public delegate void ComboChanged(int newCombo);
    public event ComboChanged OnComboChanged;
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
        _score = 0;
        _combo = 0;
    }

    /// <summary>
    /// Use this method to send a score to the score system
    /// </summary>
    public void SendScore(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        // Update combo
        if (rythmStatus != RythmButtonController.RythmButtonStatus.Miss)
        {
            _combo++;
        }
        else
        {
            _combo = 0;
        }

        if (OnComboChanged != null)
        {
            OnComboChanged(_combo);
        }

        // Update score
        int oldScore = _score;
        _score += GetScore(rythmStatus);

        if (OnScoreChanged != null)
        {
            OnScoreChanged(oldScore, _score);
        }
    }

    /// <summary>
    /// Return a score based on <see cref="RythmButtonController.RythmButtonStatus"/> and takes combos into account.s
    /// </summary>
    private int GetScore(RythmButtonController.RythmButtonStatus rythmStatus)
    {
        // When the player missed a note, the combo will be reseted and miss score will be sent.
        if (rythmStatus == RythmButtonController.RythmButtonStatus.Miss)
        {
            _combo = 0;
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
        float scoreMultiplier = Mathf.Min(_combo, MaxComboMultiplier) * ComboScoreMultiplier;
        int finalScore = (int)(baseScore * scoreMultiplier);

        // Return score
        return finalScore;
    }
}
