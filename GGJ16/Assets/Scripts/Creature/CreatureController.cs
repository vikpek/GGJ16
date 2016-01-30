using UnityEngine;
using System.Collections;
using System;

public class CreatureController : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        RythmController.Instance.OnStageReceived += OnStageReceived;
    }

    void OnDestroy()
    {
        RythmController.Instance.OnStageReceived -= OnStageReceived;
    }

    private void OnStageReceived(int stage)
    {
        GameModel.Instance.CreatureLevel = stage + 1;
    }
}
