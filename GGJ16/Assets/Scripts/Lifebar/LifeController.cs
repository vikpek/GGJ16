﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private int LifeDamage = 25;

	bool endOnce = true;

	private AudioSource audioSource;

	[SerializeField]
	private AudioClip audioclipGameOver;

    [SerializeField]
    private WinViewController _winScreen;

	// Use this for initialization
	void Awake ()
    {
        _winScreen.gameObject.SetActive(false);

        audioSource = GetComponent<AudioSource> ();
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
		if (newLife <= 0 && endOnce) {
			audioSource.PlayOneShot (audioclipGameOver);
			StartCoroutine (UnloadSceneWithDelay ());
			Destroy(GameObject.FindGameObjectWithTag("GameController"));
			endOnce = false;
		}
	}

	private IEnumerator UnloadSceneWithDelay(){
		yield return new WaitForSeconds (1);
        _winScreen.gameObject.SetActive(true);
        _winScreen.UpdateView();
    }
}
