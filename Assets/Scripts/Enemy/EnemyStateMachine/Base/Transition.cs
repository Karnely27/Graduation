using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected List<Creature> Targets { get; private set; }

    protected Creature Target { get; private set; }

    protected Spawner Spawner { get; private set; }

    protected Tower Tower { get; private set; }

    protected Enemy Enemy { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(List<Creature> targets, Spawner spawner, Creature target, Tower tower, Enemy enemy)
    {
        Targets = targets;
        Spawner = spawner;
        Target = target;
        Tower = tower;
        Enemy = enemy;
    }
}