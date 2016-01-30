using UnityEngine;
using System.Collections;

public class CircleIndicatorController : MonoBehaviour {

	[SerializeField]
	float minimumSize = 0.01f;

	[SerializeField]
	private float _indicatorSpeed = 0.01f;
	public float IndicatorSpeed
	{
		get { return _indicatorSpeed; }
		set	{ _indicatorSpeed = value;	}
	}

	void Update () {
		if (transform.localScale.x > minimumSize) {
			transform.localScale -= new Vector3 (0.01f, 0.01f, 0.01f);
		} else {
			Destroy (gameObject);
		}
	}

}
