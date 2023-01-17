using UnityEngine;

public class AttackState : State
{
    private const string _stabAttack = "Stab Attack";
    private float _lastAttackTime;

    private void Update()
    {
        if (Target != null)
        {
            if (_lastAttackTime <= 0)
            {
                Attack(Target);
                Animator.Play(_stabAttack);
                _lastAttackTime = Enemy.Delay;
            }
            _lastAttackTime -= Time.deltaTime;
        }
        if (Target == null)
        {
            if (_lastAttackTime <= 0)
            {
                AttackTower(Tower);
                Animator.Play(_stabAttack);
                _lastAttackTime = Enemy.Delay;
            }
            _lastAttackTime -= Time.deltaTime;
        }
    }

    private void Attack(Creature target)
    {
        target.ApplyDamage(Enemy.Damage);
    }

    private void AttackTower(Tower tower)
    {
        tower.ApplyDamage(Enemy.Damage);
    }
}
