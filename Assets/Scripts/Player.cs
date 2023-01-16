using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Container _container;
    [SerializeField] private int _startMoney;
    [SerializeField] private int _startCountWorker;
    [SerializeField] private int _priceWorker;
    [SerializeField] private int _startCountDiamonds;
    [SerializeField] private float _timeBetweenMiningDiamonds;
    [SerializeField] private int _startIncome;

    private int _currentCountWorkers;
    private int _currentMoney;
    private int _currentDiamonds;
    private float _currentTimeMining;
    private int _currentIncome;

    public List<Creature> CreaturesPlayer;

    public int Money => _currentMoney;

    public int Diamonds => _currentDiamonds;

    public event UnityAction<int> MoneyChanged;

    public event UnityAction<int> WorkersChanged;

    public event UnityAction<int> DiamondsChanged;

    public event UnityAction<int> IncomeChanged;

    private void Start()
    {
        _currentMoney = _startMoney;
        _currentCountWorkers = _startCountWorker;
        _currentDiamonds = _startCountDiamonds;
        _currentIncome = _startIncome;
        MoneyChanged?.Invoke(_currentMoney);
        WorkersChanged?.Invoke(_currentCountWorkers);
        DiamondsChanged?.Invoke(_currentDiamonds);
        IncomeChanged?.Invoke(_currentIncome);
    }

    private void Update()
    {
        if (_currentTimeMining <= 0)
        {
            _currentDiamonds += _currentCountWorkers;
            DiamondsChanged?.Invoke(_currentDiamonds);
            _currentTimeMining = _timeBetweenMiningDiamonds;
        }
        _currentTimeMining -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _buildingsGrid.CreatureInstalled += BuyCreature;
        _container.EnemiesDied += RestartPositionCreatures;
    }

    private void OnDisable()
    {
        _buildingsGrid.CreatureInstalled -= BuyCreature;
        _container.EnemiesDied += RestartPositionCreatures;
    }

    private void RestartPositionCreatures()
    {
        foreach (var creature in CreaturesPlayer)
        {
            creature.GetIntoStartPosition();
            creature.ResetHealth();
            creature.gameObject.SetActive(true);
        }
        _currentMoney += _currentIncome;
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void BuyWorker()
    {
        if (_priceWorker <= _currentMoney)
        {
            _currentMoney -= _priceWorker;
            MoneyChanged?.Invoke(_currentMoney);
            _currentCountWorkers++;
            WorkersChanged?.Invoke(_currentCountWorkers);
        }
    }

    public void TakeAwayDiamonds(int diamonds)
    {
        _currentDiamonds -= diamonds;
        DiamondsChanged.Invoke(_currentDiamonds);
        _currentIncome += (diamonds / 20) * 6;
        IncomeChanged?.Invoke(_currentIncome);
    }

    public void AddMoney(int reward)
    {
        _currentMoney += reward;
        MoneyChanged?.Invoke(_currentMoney);
    }

    public void BuyCreature(Creature creature)
    {
        if (_currentMoney >= creature.Price)
        {
            _currentMoney -= creature.Price;
            MoneyChanged?.Invoke(_currentMoney);
            CreaturesPlayer.Add(creature);
            creature.SetStartingPosition(creature.transform.position, creature.transform.rotation);
        }
    }
}
