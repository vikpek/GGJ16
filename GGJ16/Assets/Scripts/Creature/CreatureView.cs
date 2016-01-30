using UnityEngine;
using System.Collections;
using System;

public class CreatureView : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _creatures;

    void Awake()
    {
        GameModel.Instance.OnCreaturelevelChanged += OnCreatureLevelChanged;
    }

    void OnDestroy()
    {
        GameModel.Instance.OnCreaturelevelChanged -= OnCreatureLevelChanged;
    }

    private void OnCreatureLevelChanged(int oldLevel, int newLevel)
    {
        if (newLevel <= _creatures.Length)
        {
            for (int i = 0; i < _creatures.Length; i++)
            {
                GameObject creature = _creatures[i];
                creature.SetActive(i + 1 == newLevel);
            }
        }
    }
}
