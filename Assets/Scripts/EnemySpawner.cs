using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay = 5f;
    private float elapsedTime = 0f;
    private bool hasSpawned = false;

    void Update()
    {
        if (!hasSpawned)
        {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime >= spawnDelay)
            {
                SpawnEnemy();
                hasSpawned = true;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab is not assigned!");
        }
    }
}
