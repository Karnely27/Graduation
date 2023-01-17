using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _reward;
    [SerializeField] private int _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangetSpread;
    [SerializeField] private Slider _slider;

    private int _currentHealth;
    private bool _isAlive = true;
    private List<Creature> _creaturesPlayer;
    private Spawner _spawner;
    private Tower _tower;

    public List<Creature> Creatures => _creaturesPlayer;

    public bool IsAlive => _isAlive;

    public int Damage => _damage;

    public float Delay => _delayBetweenAttacks;

    public float Speed => _moveSpeed;

    public float TransitionRange => _transitionRange;

    public float RangetSpread => _rangetSpread;

    public int Reward => _reward;

    public Spawner Spawner => _spawner;

    public Tower Tower => _tower;

    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _slider.maxValue = _currentHealth;
        _slider.value = _currentHealth;
    }

    public void Init(Player player, Spawner spawner, Tower tower)
    {
        _creaturesPlayer = player.CreaturesPlayer;
        _spawner = spawner;
        _tower = tower;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        _slider.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            Dying?.Invoke(this);
            _isAlive = false;
            gameObject.SetActive(false);
        }
    }
}