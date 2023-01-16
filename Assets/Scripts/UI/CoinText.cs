using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinText : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _coinText;

    private void OnEnable()
    {
        _player.MoneyChanged += ChangeText;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= ChangeText;
    }

    private void ChangeText(int money)
    {
        _coinText.text = money.ToString();
    }
}
