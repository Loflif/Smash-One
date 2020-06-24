using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Shot = null;

    [SerializeField] private KeyCode UpKey = KeyCode.W;
    [SerializeField] private KeyCode RightKey = KeyCode.D;
    [SerializeField] private KeyCode DownKey = KeyCode.S;
    [SerializeField] private KeyCode LeftKey = KeyCode.A;

    [SerializeField] private float MovementSpeed = 2.0f;
   

    void Start()
    {
        if(Shot == null)
        {
            Debug.LogError("Player Shot prefab not linked");
        }
    }

    void Update()
    {
        if (Input.GetKey(UpKey) && Input.GetKey(RightKey)) Move(Direction.NORTH_EAST);
        else if (Input.GetKey(RightKey) && Input.GetKey(DownKey)) Move(Direction.SOUTH_EAST);
        else if (Input.GetKey(LeftKey) && Input.GetKey(DownKey)) Move(Direction.SOUTH_WEST);
        else if (Input.GetKey(LeftKey) && Input.GetKey(UpKey)) Move(Direction.NORTH_WEST);
        else if (Input.GetKey(UpKey)) Move(Direction.NORTH);
        else if (Input.GetKey(RightKey)) Move(Direction.EAST);
        else if (Input.GetKey(DownKey)) Move(Direction.SOUTH);
        else if (Input.GetKey(LeftKey)) Move(Direction.WEST);
    }

    private void Move(Direction pDirection)
    {
        switch (pDirection)
        {
            case Direction.NORTH:
                transform.position += Vector3.up * MovementSpeed * Time.deltaTime;
                break;
            case Direction.NORTH_EAST:
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                break;
            case Direction.EAST:
                transform.position += Vector3.right * MovementSpeed * Time.deltaTime;
                break;
            case Direction.SOUTH_EAST:
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                break;
            case Direction.SOUTH:
                transform.position += Vector3.down * MovementSpeed * Time.deltaTime;
                break;
            case Direction.SOUTH_WEST:
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                break;
            case Direction.WEST:
                transform.position += Vector3.left * MovementSpeed * Time.deltaTime;
                break;
            case Direction.NORTH_WEST:
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                break;
        }
    }

    private void Shoot(Direction pDirection)
    {
        switch (pDirection)
        {
            case Direction.NORTH:
                break;
            case Direction.NORTH_EAST:
                break;
            case Direction.SOUTH_EAST:
                break;
            case Direction.EAST:
                break;
            case Direction.SOUTH:
                break;
            case Direction.SOUTH_WEST:
                break;
            case Direction.WEST:
                break;
            case Direction.NORTH_WEST:
                break;
            
        }
    }
}
