using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Spawner _spawner;
    private List<Creature> _creatures;
    private State _currentState;
    private Enemy _enemy;
    private Tower _core;

    private void Start()
    {
        _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        _core = GameObject.FindWithTag("Core").GetComponent<Tower>();
        _enemy = GetComponent<Enemy>();
        _creatures = _enemy.Creatures;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;


        if (_currentState != null)
        {
            _currentState.Enter(_creatures, _spawner, _core, _enemy);
        }
    }

    public void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_creatures, _spawner, _core, _enemy);
    }
}
