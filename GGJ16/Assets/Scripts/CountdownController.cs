using UnityEngine;
using System.Collections;

public class CountdownController : MonoBehaviour {
	private string countdown = "";    
	private bool showCountdown = false;

	private RythmController rythmController;

	void Start(){
		rythmController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<RythmController> ();
		StartCoroutine (GetReady ());
	}

	IEnumerator GetReady ()    
	{
		showCountdown = true;    

		countdown = "3";    
		yield return new WaitForSeconds (1.5f);  

		countdown = "2";    
		yield return new WaitForSeconds (1.5f);

		countdown = "1";  
		yield return new WaitForSeconds (1.5f);

		countdown = "GO"; 
		yield return new WaitForSeconds (1.5f);

		rythmController.StartRythm ();
		showCountdown = false;
		countdown = "";  
	}
		
	void  OnGUI ()
	{
		if (showCountdown)
		{    
			GUI.color = Color.red;    
			GUI.Box (new Rect (Screen.width / 2 - 100, 50, 200, 175), "GET READY");
		
			GUI.color = Color.white;    
			GUI.Box (new Rect (Screen.width / 2 - 90, 75, 180, 140), countdown);
		}    
	}
}