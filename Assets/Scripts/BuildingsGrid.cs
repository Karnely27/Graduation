using UnityEngine;
using UnityEngine.Events;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private Vector2Int _startPositionGrid;
    [SerializeField] private Player _player;
    [SerializeField] private GameTimer _timer;
    [SerializeField] private Container _container;
    [SerializeField] private Spawner _spawner;

    private Creature[,] _grid;
    private Creature _takenCreature;

    public Vector2Int GridSize => _gridSize;

    public Vector2Int StartPositinGrid => _startPositionGrid;

    public event UnityAction<Creature> CreatureInstalled;

    private void Awake()
    {
        _grid = new Creature[_gridSize.x, _gridSize.y];
    }

    private void OnEnable()
    {
        _timer.TimeIsUp += DestroyTakenCreature;
    }

    private void OnDisable()
    {
        _timer.TimeIsUp -= DestroyTakenCreature;
    }

    private void Update()
    {
        if (_takenCreature != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                if (Input.GetMouseButtonDown(1))
                    Destroy(_takenCreature.gameObject);
                bool available = true;

                if (x < _startPositionGrid.x || x > _gridSize.x - _takenCreature.Size.x)
                    available = false;
                if (z < _startPositionGrid.y || z > _gridSize.y - _takenCreature.Size.y)
                    available = false;

                if (available && IsPlaceTaken(x, z))
                    available = false;

                _takenCreature.transform.position = new Vector3(x, 0, z);
                _takenCreature.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                    PostFlyingCreature(x, z);
            }
        }
    }

    private void StartPlacementCreature(Creature buildingPrefab)
    {
        if (_timer.TimerOn == true)
        {
            if (_takenCreature != null)
                Destroy(_takenCreature.gameObject);

            _takenCreature = Instantiate(buildingPrefab);
            _takenCreature.Init(_container, _spawner);
        }
    }

    private bool IsPlaceTaken(int placeX, int placeZ)
    {
        for (int i = 0; i < _takenCreature.Size.x; i++)
        {
            for (int j = 0; j < _takenCreature.Size.y; j++)
            {
                if (_grid[placeX + i, placeZ + j] != null)
                    return true;
            }
        }
        return false;
    }

    private void PostFlyingCreature(int placeX, int placeZ)
    {
        for (int i = 0; i < _takenCreature.Size.x; i++)
        {
            for (int j = 0; j < _takenCreature.Size.y; j++)
            {
                _grid[placeX + i, placeZ + j] = _takenCreature;
            }
        }
        _takenCreature.SetNormal();
        CreatureInstalled?.Invoke(_takenCreature);
        _takenCreature = null;
    }

    private void DestroyTakenCreature()
    {
        if (_takenCreature != null)
            Destroy(_takenCreature.gameObject);
    }

    public void CheckSolvency(Creature buildingPrefab)
    {
        if (_player.Money >= buildingPrefab.Price)
            StartPlacementCreature(buildingPrefab);
        else
        {
            if (_takenCreature != null)
                Destroy(_takenCreature.gameObject);
        }
    }
}
