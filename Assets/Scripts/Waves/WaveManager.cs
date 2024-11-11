using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaveManager : MonoBehaviour
{
    private Transform player;

    public static int enemiesAlive = 0;

    [Header("Waves")]
    [SerializeField] private int maxWavePoints = 0;
    [SerializeField] private int wavePoints = 0;
    [Space]
    [SerializeField] private float timeBetweenWaves;
    [Space]
    [SerializeField] private List<WavePoolUpdate> wavePoolData = new List<WavePoolUpdate>();
    [SerializeField] private List<SpawnPointData> spawnPoints = new List<SpawnPointData>();

    private List<SpawnableData> poolBackup = new List<SpawnableData>();
    private List<Spawnable> affordablePool = new List<Spawnable>();

    private WavePoolUpdate currentWaveData;
    
    private int wave = 0;
    private int maxRoll = 0;

    private float timer = 0f;

    private WaveState currentState;

    private enum WaveState
    {
        None,
        Intermission,
        Preparing,
        Spawning,
        Waiting
    }

    void Start()
    {
        player = FindObjectOfType<Player>().transform;

        wavePoolData.Sort((x, y) => x.wave.CompareTo(y.wave));

        StartCoroutine(WaveSetup());
    }

    void ChangeState(WaveState newState)
    {
        currentState = newState;
    }

    public void OnSpawnableDeath()
    {
        enemiesAlive -= 1;
    }

    void Update()
    {
        switch(currentState)
        {
            case WaveState.Spawning:
                timer += Time.deltaTime;

                if (timer >= currentWaveData.spawnDelay)
                {
                    SpawnEnemy();
                    timer = 0;
                }
                break;

            case WaveState.Waiting:
                if (enemiesAlive <= 0)
                {
                    StartCoroutine(Intermission());
                }
                break;
        }
    }

    void UpdateWaveData()
    {
        wave += 1;

        if (wavePoolData.Count != 0)
        {
            if (wave == wavePoolData[0].wave)
            {
                LoadNextWave();
            }
        }

        maxWavePoints += currentWaveData.pointsRate;
        wavePoints = maxWavePoints;

        affordablePool.Clear();
    }

    void LoadNextWave()
    {
        currentWaveData = wavePoolData[0];

        for (int i = 0; i < currentWaveData.addToPool.Count; i++)
        {
            poolBackup.Add(currentWaveData.addToPool[i]);
        }
        for (int i = poolBackup.Count - 1; i >= 0; i--)
        {
            for (int x = 0; x < currentWaveData.removeFromPool.Count; x++)
            {
                if (poolBackup[i] == currentWaveData.removeFromPool[x])
                {
                    poolBackup.RemoveAt(i);
                    break;
                }
            }
        }

        wavePoolData.RemoveAt(0);
    }

    IEnumerator Intermission()
    {
        ChangeState(WaveState.Intermission);

        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(WaveSetup());
    }

    IEnumerator WaveSetup()
    {
        UpdateWaveData();

        ChangeState(WaveState.Preparing);

        for (int i = 0; i < poolBackup.Count; i++)
        {
            Spawnable newEnemy = null;

            for (int x = 0; x < spawnPoints.Count; x++)
            {
                if (spawnPoints[x].CanSpawn(poolBackup[i]))
                {
                    if (newEnemy == null)
                    {
                        newEnemy = new Spawnable(poolBackup[i]);
                    }

                    newEnemy.canSpawnIn.Add(spawnPoints[x]);
                }
            }

            if (newEnemy != null)
            {
                affordablePool.Add(newEnemy);
            }

            yield return null;
        }

        UpdateAffordable();

        if (affordablePool.Count > 0)
        {
            ChangeState(WaveState.Spawning);
        }
    }

    void UpdateAffordable()
    {
        maxRoll = 0;

        for (int i = affordablePool.Count - 1; i >= 0; i--)
        {
            if (affordablePool[i].cost > wavePoints)
            {
                affordablePool.RemoveAt(i);
            }
            else
            {
                maxRoll += affordablePool[i].priority;
            }
        }
    }

    void SpawnEnemy()
    {
        int roll = RollEnemy();

        if (roll == -1)
        {
            ChangeState(WaveState.Waiting);
            return;
        }

        SetupEnemy(Instantiate(affordablePool[roll].prefab, spawnPoints[RollSpawn(affordablePool[roll])].transform.position, Quaternion.identity));
        enemiesAlive += 1;
    }

    void SetupEnemy(GameObject enemy)
    {
        enemy.GetComponent<Entity>().OnDeath += OnSpawnableDeath;
    }

    int RollSpawn(Spawnable spawnable)
    {
        int spawnMaxRoll = 0;

        for (int i = 0; i < spawnable.canSpawnIn.Count; i++)
        {
            spawnMaxRoll += spawnable.canSpawnIn[i].priority;
        }

        int currentNum = Random.Range(0, spawnMaxRoll);

        for (int i = 0; i < spawnable.canSpawnIn.Count; i++)
        {
            if (currentNum < spawnable.canSpawnIn[i].priority)
            {
                return i;
            }
            else
            {
                currentNum -= spawnable.canSpawnIn[i].priority;
            }
        }
        return -1;
    }

    int RollEnemy()
    {
        while (affordablePool.Count > 0)
        {
            int currentNum = Random.Range(0, maxRoll);

            for (int i = affordablePool.Count - 1; i >= 0 ; i--)
            {
                if (currentNum < affordablePool[i].priority)
                {
                    if (affordablePool[i].cost > wavePoints)
                    {
                        maxRoll -= affordablePool[i].priority;
                        affordablePool.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        wavePoints -= affordablePool[i].cost;
                        return i;
                    }
                }
                else
                {
                    currentNum -= affordablePool[i].priority;
                }
            }
        }

        return -1;
    }

    public class Spawnable
    {
        public GameObject prefab;

        public int cost;
        public int priority;

        public List<SpawnPointData> canSpawnIn;

        public Spawnable(SpawnableData spawnableData)
        {
            prefab = spawnableData.prefab;
            cost = spawnableData.cost;
            priority = spawnableData.priority;
            
            canSpawnIn = new List<SpawnPointData>();
        }
    }
}