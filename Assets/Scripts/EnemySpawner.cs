using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private int count;
    [SerializeField] private double delay;
    private double timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 0 && timeLeft <= 0)
        { 
            Spawn();
            count--;
            timeLeft = delay;
        }
        timeLeft -= (0.5f * Time.deltaTime);
    }

    void Spawn()
    {
        Instantiate(enemy, transform);
    }
}
