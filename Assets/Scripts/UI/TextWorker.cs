using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWorker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _player.WorkersChanged += ChangeText;
    }

    private void OnDisable()
    {
        _player.WorkersChanged -= ChangeText;
    }

    private void ChangeText(int workerCount)
    {
        _text.text = workerCount.ToString();
    }
}
