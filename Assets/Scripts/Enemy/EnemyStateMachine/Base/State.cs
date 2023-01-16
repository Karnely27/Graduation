using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected List<Creature> Targets { get; set; }

    protected Animator Animator { get; set; }

    protected Spawner Spawner { get; set; }

    protected Creature Target { get; set; }

    protected Tower Core { get; set; }

    protected Enemy Enemy { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Enter(List<Creature> targets, Spawner spawner, Tower core, Enemy enemy)
    {
        if (enabled == false)
        {
            Targets = targets;
            Spawner = spawner;
            enabled = true;
            Target = GetNearTarget();
            Core = core;
            Enemy = enemy;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(targets, spawner, Target, core, enemy);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }
        return null;
    }

    protected Creature GetNearTarget()
    {
        Creature nearCreature = new Creature();
        float shorterDistance = Mathf.Infinity;

        if (Targets.Count != 0)
        {
            foreach (Creature creature in Targets)
            {
                if (creature.IsAlive)
                {
                    if (shorterDistance > Vector3.Distance(transform.position, creature.transform.position))
                    {
                        shorterDistance = Vector3.Distance(transform.position, creature.transform.position);
                        nearCreature = creature;
                    }
                }
            }
            return nearCreature;
        }
        else
        {
            return null;
        }
    }
}
