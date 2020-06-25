using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUp;
    [SerializeField] private float areaX;
    [SerializeField] private float areaY;
    [SerializeField] private float timer;
    [SerializeField] private float currentTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = timer;
    }



    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Debug.Log("it's happening!");
            currentTimer = timer;
            SpawnPowerup();
        }
    }

    private void SpawnPowerup()
    {
        float x = UnityEngine.Random.Range(0, areaX);
        float y = -UnityEngine.Random.Range(0, areaY);

        int powerupPicker = UnityEngine.Random.Range(0, powerUp.Length);
        Instantiate(powerUp[powerupPicker], transform.position + new Vector3(x, y), Quaternion.identity);
    }

}