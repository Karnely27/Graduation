using UnityEngine;

public class MoveStateCreature : StateCreature
{
    private const string _walkForward = "Walk Forward";

    private void OnEnable()
    {
        Animator.SetBool(_walkForward, true);
    }

    private void OnDisable()
    {
        Animator.SetBool(_walkForward, false);
    }

    private void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform);
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Creature.Speed);
        }
    }
}