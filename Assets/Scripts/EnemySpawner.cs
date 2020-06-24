using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public struct SpawnerData
{
    public Transform SpawnPosition;
    public GameObject EnemyFormation;

    public float SpawnDelay;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public List<SpawnerData> Spawns = new List<SpawnerData>();

    private double timeLeft;

    void Start()
    {
        SetNextTimer();
    }

    void Update()
    {
        if(timeLeft < 0)
        {
            SpawnNext();
        }
        timeLeft -= Time.deltaTime;
    }

    void SpawnNext()
    {
        if (Spawns.Count <= 0)
            return;
        
        Instantiate(Spawns[0].EnemyFormation, Spawns[0].SpawnPosition);
        SetNextTimer();

        if (Spawns.Count > 0)
        {
            Spawns.RemoveAt(0);
        }
    }

    void SetNextTimer()
    {
        if (Spawns.Count <= 0)
            return;

        timeLeft = Spawns[0].SpawnDelay;
    }
}
