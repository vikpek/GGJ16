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
		do{
			if (rythmButtons[Random.Range(0, rythmButtons.Length)].GetComponent<RythmButtonController> ().GetRythmStatus () == RythmButtonController.RythmButtonStatus.Passive) {
				rythmButtons[Random.Range(0, rythmButtons.Length)].GetComponent<RythmButtonController> ().ActivateRythmButton ();
			}
			break;
		}while(true);

		if (currentDuration > 0) {
			currentDuration--;
		} else {
			currentDuration = durationPerStage;
			escalationSpeed -= escalationSpeed * 0.1f;
			print(escalationSpeed);
			CancelInvoke ("RythmTick");
			StartRythm ();
		}
	
	}
}
