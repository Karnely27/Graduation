using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreating : MonoBehaviour
{
    [SerializeField] private GameObject _gridPrefab;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    private void Start()
    {
        for (int x = 0; x < _buildingsGrid.GridSize.x; x++)
        {
            if (x >= _buildingsGrid.StartPositinGrid.x)
            {
                for (int y = 0; y < _buildingsGrid.GridSize.y - 1; y++)
                {
                    if (y + 1 >= _buildingsGrid.StartPositinGrid.y)
                    {
                        Instantiate(_gridPrefab, new Vector3(x - 1, 0, y), Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}
