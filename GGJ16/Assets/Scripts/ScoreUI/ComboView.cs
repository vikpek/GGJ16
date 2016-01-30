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
    }
}