using System.Collections;
using UnityEngine;

public class AttackStateCreature : StateCreature
{
    private const string _stabAttack = "Stab Attack";

    private void OnEnable()
    {
        StartCoroutine(Attack(Target));
    }

    private void OnDisable()
    {
        StopCoroutine(Attack(Target));
    }

    private IEnumerator Attack(Enemy target)
    {
        while (Target != null)
        {
            target.ApplyDamage(Creature.Damage);
            Animator.Play(_stabAttack);
            yield return new WaitForSeconds(Creature.Delay);
        }
    }
}