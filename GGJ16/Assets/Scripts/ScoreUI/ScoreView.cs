using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

	// Use this for initialization
	void Awake ()
    {
        GameModel.Instance.OnScoreChanged += OnScoreChanged;
	}

    void Start()
    {
        Reset();
    }

    void OnDestroy()
    {
        GameModel.Instance.OnScoreChanged -= OnScoreChanged;
    }

    private void Reset()
    {
        _scoreText.text = "0";
    }

    private void OnScoreChanged(int oldScore, int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
