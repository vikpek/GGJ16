using UnityEngine;
using UnityEngine.UI;

public class LifeView : MonoBehaviour
{
    [SerializeField]
    private Image _lifebar;

	// Use this for initialization
	void Awake ()
    {
        GameModel.Instance.OnLifeChanged += OnLifeChanged;
	}

    void OnDestroy()
    {
        GameModel.Instance.OnLifeChanged -= OnLifeChanged;
    }

    private void OnLifeChanged(int oldLife, int newLife, float lifePercentage)
    {
        _lifebar.fillAmount = lifePercentage;
    }
}
