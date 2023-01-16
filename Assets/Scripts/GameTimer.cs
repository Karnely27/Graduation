using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _constructionStageTime;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Container _container;
    [SerializeField] private GameObject _grid;

    private bool _timerOn = true;
    private float _timeLeft = 0f;

    public bool TimerOn => _timerOn;

    public event UnityAction TimeIsUp;

    private void OnEnable()
    {
        _container.EnemiesDied += ReloadTimer;
    }

    private void OnDisable()
    {
        _container.EnemiesDied -= ReloadTimer;
    }

    private void Start()
    {
        _timeLeft = _constructionStageTime;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                UpdateTimeText();
            }
            else
            {             
                _timerOn = false;
                _grid.SetActive(false);
                TimeIsUp?.Invoke();
            }
        }
        if(!_timerOn)
        {
            _timeLeft += Time.deltaTime;
            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
   
    public void ReloadTimer()
    {
        _timeLeft = _constructionStageTime;
        _grid.SetActive(true);
        _timerOn = true;
    }
}
