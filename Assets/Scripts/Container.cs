using UnityEngine;
using UnityEngine.Events;

public class Container : MonoBehaviour
{
    public event UnityAction EnemiesDied;

    private void Update()
    {
        if (CheckSurvivingEnemies() == transform.childCount)
            DestroyEnemies();
        if (transform.childCount == 0)
        {
            EnemiesDied?.Invoke();
            enabled = false;
        }
    }

    private int CheckSurvivingEnemies()
    {
        int deadEnemies = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Enemy>().IsAlive == false)
                deadEnemies++;
        }
        return deadEnemies;
    }

    private void DestroyEnemies()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
