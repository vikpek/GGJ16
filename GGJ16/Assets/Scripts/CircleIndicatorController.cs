using UnityEngine;
using System.Collections;

public class CircleIndicatorController : MonoBehaviour {

	private float _minimumSize = 0.02f;

	private float _indicatorSpeed = 0.005f;
	public float IndicatorSpeed
	{
		get { return _indicatorSpeed; }
		set	{ _indicatorSpeed = value;	}
	}

	void Update () {
		if (transform.localScale.x > _minimumSize || transform.localScale.y > _minimumSize) {
			transform.localScale -= new Vector3(_indicatorSpeed, _indicatorSpeed, _indicatorSpeed);
		} else {
			Destroy (gameObject);
		}
	}

}
