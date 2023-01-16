using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _lastAttackTime;

    private void Update()
    {
        if (Target != null)
        {
            if (_lastAttackTime <= 0)
            {
                Attack(Target);
                Animator.Play("Stab Attack");
                _lastAttackTime = Enemy.Delay;
            }
            _lastAttackTime -= Time.deltaTime;
        }
        if (Target == null)
        {
            if (_lastAttackTime <= 0)
            {
                AttackCore(Core);
                Animator.Play("Stab Attack");
                _lastAttackTime = Enemy.Delay;
            }
            _lastAttackTime -= Time.deltaTime;
        }
    }

    private void Attack(Creature target)
    {
        target.ApplyDamage(Enemy.Damage);
    }

    private void AttackCore(Tower core)
    {
        core.ApplyDamage(Enemy.Damage);
    }
}
