using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform player;

    private ObjectPool enemyPool = new ObjectPool();

    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        int enemySpawn = Random.Range(0, enemyPrefab.Length);

        GameObject enemy = enemyPool.GetObject(enemyPrefab[enemySpawn]);

        enemy.transform.position = spawnPoint.position;

        if (enemy.TryGetComponent(out IDamageable damageable))
        {
            damageable.Heal(damageable.MaxHealth);
        }

        if (enemy.TryGetComponent(out ChaseTarget chaseTarget))
        {
            if (player != null)
            {
                chaseTarget.target = player;
            }
        }
    }
}