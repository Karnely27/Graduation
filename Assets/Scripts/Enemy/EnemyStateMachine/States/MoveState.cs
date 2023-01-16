using UnityEngine;

public class MoveState : State
{
    private void OnEnable()
    {
        Animator.SetBool("Walk Forward", true);
    }

    private void OnDisable()
    {
        Animator.SetBool("Walk Forward", false);
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
            transform.LookAt(Core.transform);
            transform.position = Vector3.MoveTowards(transform.position, Core.transform.position, Time.deltaTime * Enemy.Speed);
        }
    }
}
