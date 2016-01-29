using UnityEngine;
using System.Collections;

public class RythmButtonController : MonoBehaviour {

	public enum RythmButtonStatus{Passive, Perfect, Great, Ok, Miss};

	private RythmButtonStatus status;

	void Start () {
		status = RythmButtonStatus.Passive;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
