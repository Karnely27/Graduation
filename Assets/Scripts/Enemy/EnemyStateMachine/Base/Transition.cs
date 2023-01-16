using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected List<Creature> Targets { get; private set; }

    protected Creature Target { get; private set; }

    protected Spawner Spawner { get; private set; }

    protected Tower Core { get; private set; }

    protected Enemy Enemy { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(List<Creature> targets, Spawner spawner, Creature target, Tower core, Enemy enemy)
    {
        Targets = targets;
        Spawner = spawner;
        Target = target;
        Core = core;
        Enemy = enemy;
    }
}


