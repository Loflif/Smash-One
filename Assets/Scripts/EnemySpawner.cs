using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public struct SpawnerData
{
    public GameObject EnemyFormation;

    public GameObject PowerUp;

    public float SpawnDelay;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private bool InfiniteSpawner = true;
    [SerializeField] private List<Transform> EnemySpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> PowerupSpawnPoints = new List<Transform>();

    [SerializeField] public List<SpawnerData> Spawns = new List<SpawnerData>();

    private double timeLeft;

    private SpawnerData nextSpawn;

    private bool lastSpawnSet = false;

    void Start()
    {
        if(InfiniteSpawner)
        {
            SetNextSpawnRandom();
        }
        else
        {
            SetNextSpawn();
        }
    }

    void Update()
    {
        if(timeLeft < 0)
        {
            SpawnNext();
        }

        timeLeft -= Time.deltaTime;
    }

    private void SpawnNext()
    {
        Instantiate(nextSpawn.EnemyFormation, GetRandomEnemySpawnPoint());
        if(nextSpawn.PowerUp != null)
        {
            Instantiate(nextSpawn.PowerUp, GetRandomPowerupSpawnPoint());
        }
        if (InfiniteSpawner)
        {
            SetNextSpawnRandom();
        }
        else
        {
            SetNextSpawn();
        }
    }

    private void SetNextSpawn()
    {
        if(lastSpawnSet)
            this.enabled = false;
        if (Spawns.Count <= 0)
            return;

        nextSpawn = Spawns[0];
        
        Spawns.RemoveAt(0);
        if (Spawns.Count <= 0)
        {
            lastSpawnSet = true;
        }

        timeLeft = nextSpawn.SpawnDelay;
    }

    private void SetNextSpawnRandom()
    {
        int randomSpawnCount = Random.Range(0, Spawns.Count);
        nextSpawn = Spawns[randomSpawnCount];
        timeLeft = nextSpawn.SpawnDelay;
    }

    private Transform GetRandomEnemySpawnPoint()
    {
        if(EnemySpawnPoints.Count < 0)
        {
            Debug.LogWarning("Enemy Spawn Points not Set");
            return transform;
        }
        int randomSpawnPointCount = Random.Range(0, EnemySpawnPoints.Count);
        return EnemySpawnPoints[randomSpawnPointCount];
    }

    private Transform GetRandomPowerupSpawnPoint()
    {
        if (PowerupSpawnPoints.Count < 0)
        {
            Debug.LogWarning("Powerup Spawn Points not Set");
            return transform;
        }
        int randomSpawnPointCount = Random.Range(0, PowerupSpawnPoints.Count);
        return PowerupSpawnPoints[randomSpawnPointCount];
    }
}
