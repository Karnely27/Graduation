using UnityEngine;

public abstract class TransitionCreature : MonoBehaviour
{
    [SerializeField] private StateCreature _targetState;

    protected Spawner Spawner { get; private set; }

    protected Enemy Target { get; private set; }

    protected Creature Creature { get; private set; }

    public StateCreature TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Enemy target, Spawner spawner, Creature creature)
    {
        Target = target;
        Spawner = spawner;
        Creature = creature;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
