using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    [SerializeField] private string _creatureName;
    [SerializeField] private Color _color;
    [SerializeField] Vector2Int _sizeGizmos;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _price;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAttacks;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangetSpread;

    private int _currentHealth;
    private Vector3 _startPosotion;
    private Quaternion _startRotation;
    private bool _isAlive = true;

    public Vector2Int Size => _sizeGizmos;

    public string CreatureName => _creatureName;

    public int MaxHealth => _maxHealth;

    public int Price => _price;

    public int Damage => _damage;

    public float Delay => _delayBetweenAttacks;

    public float Speed => _moveSpeed;

    public float TransitionRange => _transitionRange;

    public float RangetSpread => _rangetSpread;

    public bool IsAlive => _isAlive;

    public event UnityAction<Creature> DyingCreature;

    private void Start()
    {
        _currentHealth = _maxHealth; 
        _slider.maxValue = _maxHealth;
        _slider.value = _currentHealth;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _sizeGizmos.x; i++)
        {
            for (int j = 0; j < _sizeGizmos.y; j++)
            {
                Gizmos.color = _color;
                Gizmos.DrawCube(transform.position + new Vector3(j - 0.5f, 0, i - 0.5f), new Vector3(1f, 0.1f, 1f));
            }
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        _slider.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            _isAlive = false;
            DyingCreature?.Invoke(this);
            gameObject.SetActive(false);
        }
    }

    public void SetTransparent(bool avalible)
    {
        if (avalible)
            _renderer.material.color = Color.green;
        else
            _renderer.material.color = Color.red;
    }

    public void SetNormal()
    {
        _renderer.material.color = Color.white;
    }

    public void SetStartingPosition(Vector3 startPosition, Quaternion startRotation)
    {
        _startPosotion = startPosition;
        _startRotation = startRotation;
    }

    public void GetIntoStartPosition()
    {
        transform.position = _startPosotion;
        transform.rotation = _startRotation;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        _slider.value = _currentHealth;
        _isAlive = true;
    }
}
