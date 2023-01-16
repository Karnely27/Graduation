using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncomeText : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.IncomeChanged += ChangeText;
    }

    private void OnDisable()
    {
        _player.IncomeChanged -= ChangeText;
    }

    private void ChangeText(int income)
    {
        _text.text = income.ToString();
    }
}
