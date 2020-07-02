using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float MaxDistanceX, MaxDistanceY;
    public float speed;
    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Player.transform.position - transform.position;
        if (Math.Abs(difference.x) > MaxDistanceX)
            transform.position += new Vector3(difference.normalized.x, 0, 0) * speed * Time.deltaTime;
        if (Math.Abs(difference.y) > MaxDistanceY)
            transform.position += new Vector3(0, difference.normalized.y, 0) * speed * Time.deltaTime;
    }
}
