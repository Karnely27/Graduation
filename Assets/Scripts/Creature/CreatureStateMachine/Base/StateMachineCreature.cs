using UnityEngine;

[RequireComponent(typeof(Creature))]
public class StateMachineCreature : MonoBehaviour
{
    [SerializeField] private StateCreature _firstState;

    private Creature _creature;
    private StateCreature _currentState;
    private Container _container;
    private Spawner _spawner;

    private void Start()
    {
        _creature = GetComponent<Creature>();
        _container = _creature.Container;
        _spawner = _creature.Spawner;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        StateCreature nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(StateCreature nextState)
    {
        if (_currentState != null)
            _currentState.Exit();
        _currentState = nextState;


        if (_currentState != null)
            _currentState.Enter(_container, _spawner, _creature);
    }

    public void Reset(StateCreature startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_container, _spawner, _creature);
    }
}
