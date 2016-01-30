using UnityEngine;
using System.Collections;

public class CircleIndicatorController : MonoBehaviour {

	[SerializeField]
	float indicatorSpeed = 0.01f;

	[SerializeField]
	float minimumSize = 0.01f;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if (transform.localScale.x > minimumSize) {
			transform.localScale -= new Vector3 (indicatorSpeed, indicatorSpeed, indicatorSpeed);
		} else {
			Destroy (gameObject);
		}
	}

}
