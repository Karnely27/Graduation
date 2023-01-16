using UnityEngine;

public class DistanceTransitionCreature : TransitionCreature
{
    private float _transitionRange;

    private void Start()
    {
        _transitionRange = Creature.TransitionRange;
        _transitionRange += Random.Range(-Creature.RangetSpread, Creature.RangetSpread);
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector3.Distance(transform.position, Target.transform.position) < _transitionRange)
                NeedTransit = true;
        }
    }
}
