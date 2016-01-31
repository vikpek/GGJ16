using UnityEngine;

public class GameModel : MonoBehaviour
{
    [SerializeField]
    private int MaxLife = 100;

    [SerializeField]
    private int HealValue = 3;

    [SerializeField]
    private int[] NextLevel;

    #region Properties
    private int _combo;
    public int Combo
    {
        get { return _combo; }
        set
        {
            if (value > _combo)
            {
                Life += (value - _combo) * HealValue;
            }

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
                OnLifeChanged(_life, value, (value/(float)MaxLife));
            }
            _life = Mathf.Min(MaxLife, value);
        }
    }

    private int _creatureLevel;
    public int CreatureLevel
    {
        get { return _creatureLevel; }
        set
        {
            if (OnCreaturelevelChanged != null)
            {
                OnCreaturelevelChanged(_score, value);
            }
            _creatureLevel = value;
        }
    }

    private int _experiencePoints;
    public int ExperiencePoints
    {
        get { return _experiencePoints; }
        set
        {
            // Setup values
            int oldExperiencePoints = _experiencePoints;
            int newExperiencePoints = value;
            float percentage = 1.0f;

            // Calculate values when a next level is available
            if (CreatureLevel < NextLevel.Length)
            {
                int requiredExpForLvlUp = NextLevel[CreatureLevel];

                // Cap values
                percentage = Mathf.Min(value / requiredExpForLvlUp, 1.0f);
                newExperiencePoints = Mathf.Min(value, requiredExpForLvlUp);
            }

            // Assign values
            if (_experiencePoints != newExperiencePoints)
            {
                if (OnExperiencePointsChanged != null)
                {
                    OnExperiencePointsChanged(oldExperiencePoints, newExperiencePoints, percentage);
                }
                _experiencePoints = newExperiencePoints;
            }
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

    public delegate void CreatureLevelChanged(int oldLevel, int newLevel);
    public event CreatureLevelChanged OnCreaturelevelChanged;

    public delegate void ExperiencePointsChanged(int oldExp, int newExp, float expPercentage);
    public event ExperiencePointsChanged OnExperiencePointsChanged;
    #endregion

    #region Singleton
    private static GameModel instance = null;

    // Game Instance Singleton
    public static GameModel Instance
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
    }

    private void Start()
    {
        Reset();
    }
    #endregion

    public void Reset()
    {
        Instance.Score = 0;
        Instance.Combo = 0;
        Instance.Life = MaxLife;
        Instance.CreatureLevel = 1;
        Instance.ExperiencePoints = 0;
    }
}
