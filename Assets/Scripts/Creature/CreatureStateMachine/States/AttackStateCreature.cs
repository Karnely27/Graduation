using UnityEngine;

public class AttackStateCreature : StateCreature
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
                _lastAttackTime = Creature.Delay;
            }
            _lastAttackTime -= Time.deltaTime;
        }
    }

    private void Attack(Enemy target)
    {
        target.ApplyDamage(Creature.Damage);
    }
}
