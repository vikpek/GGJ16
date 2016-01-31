using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ComboView : MonoBehaviour
{
    [SerializeField]
    private Text _comboText;

	// Use this for initialization
	void Awake ()
    {
        GameModel.Instance.OnComboChanged += OnComboChanged;
	}

    void Start()
    {
        Reset();
    }

    void OnDestroy()
    {
        GameModel.Instance.OnComboChanged -= OnComboChanged;
    }

    private void Reset()
    {
        _comboText.text = "x0";
    }

    private void OnComboChanged(int newCombo)
    {
        _comboText.text = "x" + newCombo.ToString();
        _comboText.transform.localScale = Vector3.one;

        if (newCombo != 0)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_comboText.transform.DOScale(1.3f, 0.2f));
            sequence.Append(_comboText.transform.DOScale(Vector3.one, 0.1f));
        }
    }
}