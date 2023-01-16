using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _containerParent;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Player _player;
    [SerializeField] private GameTimer _timer;
    [SerializeField] private Container _container;

    private bool _isWaveReady = false;
    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private int _templateSpawnPoint = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;

    public bool IsWaveReady => _isWaveReady;

    private void OnEnable()
    {
        _timer.TimeIsUp += StartWave;
        _container.EnemiesDied += TurnOffWaveReadiness;
    }

    private void OnDisable()
    {
        _timer.TimeIsUp -= StartWave;
        _container.EnemiesDied -= TurnOffWaveReadiness;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;
        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count >= _currentWaveNumber + 1)
                _templateSpawnPoint = 0;
            _currentWave = null;
            _isWaveReady = true;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoints[_templateSpawnPoint].position, Quaternion.identity, _containerParent).GetComponent<Enemy>();
        _templateSpawnPoint++;
        _container.enabled = true;
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void StartWave()
    {
        if (_spawned == 0)
            SetWave(_currentWaveNumber);
        else
            NextWave();
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _player.AddMoney(enemy.Reward);
    }

    private void TurnOffWaveReadiness()
    {
        _isWaveReady = false;
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}

