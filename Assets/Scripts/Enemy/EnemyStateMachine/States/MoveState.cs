using UnityEngine;

public class MoveState : State
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
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Enemy.Speed);
        }
        if (Target == null)
        {
            transform.LookAt(Tower.transform);
            transform.position = Vector3.MoveTowards(transform.position, Tower.transform.position, Time.deltaTime * Enemy.Speed);
        }
    }
}
