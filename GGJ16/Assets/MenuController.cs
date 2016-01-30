using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void LoadGame()
	{
		SceneManager.LoadScene ("InGame");
	}
}
