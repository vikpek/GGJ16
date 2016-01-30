using UnityEngine;
using System.Collections;
using System;

public class CreatureView : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _creatures;

    [SerializeField]
    private GameObject _creatureChangeEffect;

    void Awake()
    {
        GameModel.Instance.OnCreaturelevelChanged += OnCreatureLevelChanged;
    }

    void OnDestroy()
    {
        GameModel.Instance.OnCreaturelevelChanged -= OnCreatureLevelChanged;
    }

    private void OnCreatureLevelChanged(int oldLevel, int newLevel)
    {
        if (newLevel <= _creatures.Length)
        {
            float delay = 0;

            if (newLevel > 1)
            {
                delay = 0.75f;
                _creatureChangeEffect.GetComponent<Animator>().SetTrigger("Activate");
            }

            StartCoroutine(UpdateCreature(newLevel, delay));
        }
    }

    private IEnumerator UpdateCreature(int newLevel, float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < _creatures.Length; i++)
        {
            GameObject creature = _creatures[i];
            creature.SetActive(i + 1 == newLevel);
        }
    }
}
