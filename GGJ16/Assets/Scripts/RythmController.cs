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


	void Start(){
		currentDuration = durationPerStage;
		StartRythm ();
		rythmButtons = GameObject.FindGameObjectsWithTag ("RythmButton");
	}

	void StartRythm(){
		InvokeRepeating("RythmTick", 0.0f, escalationSpeed);
	}

	void RythmTick(){

//		foreach(GameObject r in rythmButtons){

		int randomRythmButton = Random.Range (0, rythmButtons.Length);
		if (rythmButtons[randomRythmButton].GetComponent<RythmButtonController> ().GetRythmStatus () == RythmButtonController.RythmButtonStatus.Passive) {
			rythmButtons[randomRythmButton].GetComponent<RythmButtonController> ().ActivateRythmButton ();
		}
	
		

		if (currentDuration > 0) {
			currentDuration--;
		} else {
			currentDuration = durationPerStage;
			escalationSpeed -= escalationSpeed * 0.1f;
			CancelInvoke ("RythmTick");
			StartRythm ();
		}
	
	}
}
