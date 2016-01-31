﻿using UnityEngine;
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

	public void UpdateView()
    {
        GameObject creature = CreatureView.ActiveCreature();
        creature.transform.parent = CreaturePosition;
        creature.transform.localPosition = Vector3.zero;
        creature.transform.localScale = Vector3.one * 20;

        FinalScore.text = GameModel.Instance.Score.ToString();

        if (GameModel.Instance.CreatureLevel > 8)
        {
            DescriptionText.text = "It's over 9.000!!!";
        }
        else
        {
            DescriptionText.text = "Thank's for summoning!";
        }
    }

    public void SwitchToStartScreen()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
