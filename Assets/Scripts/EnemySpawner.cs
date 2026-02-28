using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float initialDelay = 30f;
    [SerializeField] private float waveCooldown = 30f;
    [SerializeField] private int maxPerWave = 8;

    private float timer;
    private int waveNumber;
    private bool started;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.State != GameState.Playing)
            return;

        timer += Time.deltaTime;

        if (!started)
        {
            if (timer >= initialDelay)
            {
                started = true;
                timer = 0f;
                SpawnWave();
            }
            return;
        }

        if (timer >= waveCooldown)
        {
            timer = 0f;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        waveNumber++;
        int count = Mathf.Min(waveNumber, maxPerWave);

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned!");
            return;
        }

        Vector3 pos = transform.position;
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            pos = point.position;
        }

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
