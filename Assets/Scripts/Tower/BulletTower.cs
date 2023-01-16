using UnityEngine;

public class BulletTower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _particleSystem;

    private int _damage;
    private Enemy _enemy;

    private void Update()
    {
        transform.LookAt(_enemy.transform.position);
        transform.position = Vector3.Lerp(transform.position, _enemy.transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.ApplyDamage(_damage);
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Init(Enemy enemy, int damage)
    {
        _damage = damage;
        _enemy = enemy;
    }
}
