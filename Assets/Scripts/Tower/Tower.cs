using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _cooldown;
    [SerializeField] private Container _container;
    [SerializeField] private Player _player;
    [SerializeField] private int _priceLevelUp;
    [SerializeField] private int _levelUpHealth;
    [SerializeField] private int _levelUpDamage;
    [SerializeField] private BulletTower _bullet;
    [SerializeField] private Transform _shootPoint;

    private float _currentCooldown;
    private int _currentHealth;

    public int MaxHealth => _maxHealth;

    public int LevelUpDamage => _levelUpDamage;

    public int LevelUpHealth => _levelUpHealth;

    public int PriceLevelUp => _priceLevelUp;

    public int Damage => _damage;

    public event UnityAction<int> HealthChanged;

    private void Start()
    {
        _currentHealth = _maxHealth;
        HealthChanged?.Invoke(_currentHealth);
    }

    private void Update()
    {
        if (_currentCooldown <= 0)
            SearchTarget();
        if (_currentCooldown > 0)
            _currentCooldown -= Time.deltaTime;
    }

    private void SearchTarget()
    {
        Enemy nearestEnemy = null;
        float nearestEnemyDistance = Mathf.Infinity;
        List<Enemy> targets = new List<Enemy>();

        if (_container.transform.childCount != 0)
        {
            for (int i = 0; i < _container.transform.childCount; i++)
            {
                targets.Add(_container.transform.GetChild(i).GetComponent<Enemy>());
            }

            foreach (Enemy enemy in targets)
            {
                if (enemy.IsAlive == true)
                {
                    float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (currentDistance < nearestEnemyDistance && currentDistance < _range)
                    {
                        nearestEnemy = enemy;
                        nearestEnemyDistance = currentDistance;
                    }
                }
            }
        }
        if (nearestEnemy != null)
            Shoot(nearestEnemy);
    }

    private void Shoot(Enemy enemy)
    {
        _currentCooldown = _cooldown;
        BulletTower bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        bullet.Init(enemy, _damage);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            //gameOver
        }
        HealthChanged?.Invoke(_currentHealth);
    }

    public void LevelUp()
    {
        if (_player.Diamonds >= _priceLevelUp)
        {
            _player.TakeAwayDiamonds(_priceLevelUp);
            _maxHealth += _levelUpHealth;
            _currentHealth += _levelUpHealth;
            HealthChanged?.Invoke(_currentHealth);
            _damage += _levelUpDamage;
        }

    }
}
