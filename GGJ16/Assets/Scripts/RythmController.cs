using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RythmController : MonoBehaviour {

	[SerializeField]
	float durationPerStage = 3;
	float currentDuration;
	[SerializeField]
	float escalationSpeed = 1.0f;

	GameObject[] rythmButtons;

	public delegate void StageReceived(int stage);
	public event StageReceived OnStageReceived;

    #region Singleton pattern
    private static RythmController instance = null;

    // Game Instance Singleton
    public static RythmController Instance
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
    #endregion

    void Start(){
		currentDuration = durationPerStage;
		rythmButtons = GameObject.FindGameObjectsWithTag ("RythmButton");

	}

	public void StartRythm(){
		StartCoroutine (ProcessStages ());
	}

	void RythmTick(){

		int randomRythmButton = Random.Range (0, rythmButtons.Length);
		if (rythmButtons[randomRythmButton].GetComponent<RythmButtonController> ().GetRythmStatus () == RythmButtonController.RythmButtonStatus.Passive) {
			StartCoroutine (ActiveWithDelay (randomRythmButton));
		}

//		if (currentDuration > 0) {
//			currentDuration--;
//		} else {
//			currentDuration = durationPerStage;
//			CancelInvoke ("RythmTick");
//		}
	}

	IEnumerator ActiveWithDelay(int rythmButtonIndex){
		yield return new WaitForSeconds (0.3f);  
		rythmButtons[rythmButtonIndex].GetComponent<RythmButtonController> ().ActivateRythmButton ();
	}

	IEnumerator ProcessStages()
	{
		print ("Stage0");
		currentDuration = 13;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.9f);
		SendStage (0);
		yield return new WaitForSeconds (13f);  
		CancelInvoke ("RythmTick");

		print ("Stage01");
		currentDuration = 11;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.8f);
		SendStage (1);
		yield return new WaitForSeconds (11f);  
		CancelInvoke ("RythmTick");

		print ("Stage02");
		currentDuration = 22;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.7f);
		SendStage (2);
		yield return new WaitForSeconds (22f);  
		CancelInvoke ("RythmTick");

		print ("Stage03");
		currentDuration = 23;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.6f);
		SendStage (3);
		yield return new WaitForSeconds (23f);  
		CancelInvoke ("RythmTick");

		print ("Stage04");
		currentDuration = 25;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.5f);
		SendStage (4);
		yield return new WaitForSeconds (25f);  
		CancelInvoke ("RythmTick");


		print ("Stage05");
		currentDuration = 30;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.4f);
		SendStage (5);
		yield return new WaitForSeconds (30f);
		CancelInvoke ("RythmTick");


		print ("Stage06");
		currentDuration = 40;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.3f);
		SendStage (6);
		yield return new WaitForSeconds (40f);  
		CancelInvoke ("RythmTick");


		print ("Stage07");
		currentDuration = 50;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.2f);
		SendStage (7);
		yield return new WaitForSeconds (50f);  
		CancelInvoke ("RythmTick");

		print ("Stage08");
		currentDuration = 50;
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed*0.1f);
		SendStage (8);
		yield return new WaitForSeconds (50f);  
		CancelInvoke ("RythmTick");
	}

		
	/// </summary>
	public void SendStage(int stage)
	{
		// Sent event
		if (OnStageReceived != null)
		{
			OnStageReceived(stage);
		}
	}


}
