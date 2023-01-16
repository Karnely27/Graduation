using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDiamonds : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.DiamondsChanged += ChangeText;
    }

    private void OnDisable()
    {
        _player.DiamondsChanged -= ChangeText;
    }

    private void ChangeText(int diamondsCount)
    {
        _text.text = diamondsCount.ToString();
    }
}
