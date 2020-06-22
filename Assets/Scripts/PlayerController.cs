using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode UpKey = KeyCode.W;
    [SerializeField] private KeyCode RightKey = KeyCode.D;
    [SerializeField] private KeyCode DownKey = KeyCode.S;
    [SerializeField] private KeyCode LeftKey = KeyCode.A;

    [SerializeField] private float MovementSpeed = 2.0f;
   

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(UpKey) && Input.GetKeyDown(RightKey))
        {

        }
    }

    private Vector2 Move()
    {

    }
}
