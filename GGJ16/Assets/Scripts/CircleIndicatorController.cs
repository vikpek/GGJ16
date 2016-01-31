using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircleIndicatorController : MonoBehaviour
{
    [SerializeField]
    private GameObject _circleIndicatorImage;

    [SerializeField]
    private float _originScale;

    private float _minimumSize = 0.02f;

	private float _indicatorSpeed = 0.005f;
	public float IndicatorSpeed
	{
		get { return _indicatorSpeed; }
		set	{ _indicatorSpeed = value;	}
	}

    void Start()
    {
        InUse = false;
    }

	void Update () {
		if (transform.localScale.x > _minimumSize || transform.localScale.y > _minimumSize) {
			transform.localScale -= new Vector3(_indicatorSpeed, _indicatorSpeed, _indicatorSpeed);
		} else
        {
            InUse = false;
		}
	}

    private bool _inUse;
    public bool InUse
    {
        get { return _inUse; }
        set
        {
            _inUse = value;

            if (value)
            {
                transform.localScale = Vector3.one * _originScale;
            }

            _circleIndicatorImage.SetActive(value);
        }
    }

}
