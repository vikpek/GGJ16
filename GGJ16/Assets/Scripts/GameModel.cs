using UnityEngine;

public class GameModel
{
    [SerializeField]
    private int MaxLife = 100;

    #region Properties
    private int _combo;
    public int Combo
    {
        get { return _combo; }
        set
        {
            _combo = value;
            if (OnComboChanged != null)
            {
                OnComboChanged(_combo);
            }
        }
    }

    private int _score;
    public int Score
    {
        get { return _score; }
        set
        {
            if (OnScoreChanged != null)
            {
                OnScoreChanged(_score, value);
            }
            _score = value;
        }
    }

    private int _life;
    public int Life
    {
        get { return _life; }
        set
        {
            if (OnLifeChanged != null)
            {
                OnLifeChanged(_life, value, (value/MaxLife));
            }
            _life = value;
        }
    }

    #endregion

    #region Delegate
    public delegate void ScoreChanged(int oldScore, int newScore);
    public event ScoreChanged OnScoreChanged;

    public delegate void ComboChanged(int newCombo);
    public event ComboChanged OnComboChanged;

    public delegate void LifeChanged(int oldLife, int newLife, float lifePercentage);
    public event LifeChanged OnLifeChanged;
    #endregion

    #region Singleton
    private static GameModel instance = null;

    private GameModel()
    {

    }

    public static GameModel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameModel();
            }
            return instance;
        }
    }
    #endregion

    public void Init()
    {
        Reset();
    }

    public void Reset()
    {
        Instance.Score = 0;
        Instance.Combo = 0;
        Instance.Life = MaxLife;
    }
}
