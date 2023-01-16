using System.Collections.Generic;
using UnityEngine;

public abstract class StateCreature : MonoBehaviour
{
    [SerializeField] private List<TransitionCreature> _transitions;

    protected Animator Animator { get; set; }

    protected Container Container { get; set; }

    protected Spawner Spawner { get; set; }

    protected Enemy Target { get; set; }

    protected Creature Creature { get; set; }

    private void Awake()
    {
        Animator = transform.GetComponentInChildren<Animator>();
    }

    public void Enter(Container container, Spawner spawner, Creature creature)
    {
        if (enabled == false)
        {
            Container = container;
            Spawner = spawner;
            Target = GetNearTarget();
            Creature = creature;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target, Spawner, Creature);
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

    public StateCreature GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }
        return null;
    }

    protected Enemy GetNearTarget()
    {
        Enemy nearEnemy = new Enemy();
        float shorterDistance = Mathf.Infinity;
        List<Enemy> targets = new List<Enemy>();

        if (Container.transform.childCount != 0)
        {
            for (int i = 0; i < Container.transform.childCount; i++)
            {
                targets.Add(Container.transform.GetChild(i).GetComponent<Enemy>());
            }

            foreach (Enemy enemy in targets)
            {
                if (enemy.IsAlive == true)
                {
                    if (shorterDistance > Vector3.Distance(transform.position, enemy.transform.position))
                    {
                        shorterDistance = Vector3.Distance(transform.position, enemy.transform.position);
                        nearEnemy = enemy;
                    }
                }
            }
            return nearEnemy;
        }
        else
        {
            return null;
        }

    }
}
