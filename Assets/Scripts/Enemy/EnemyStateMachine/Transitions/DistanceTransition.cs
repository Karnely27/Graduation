using UnityEngine;

public class DistanceTransition : Transition
{
    private float _transitionRange;

    private void Start()
    {
        _transitionRange = Enemy.TransitionRange;
        _transitionRange += Random.Range(-Enemy.RangetSpread, Enemy.RangetSpread);
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
                NeedTransit = true;
        }
        if (Target == null)
        {
            if (Vector3.Distance(transform.position, Core.transform.position) < _transitionRange)
                NeedTransit = true;
        }
    }
}
