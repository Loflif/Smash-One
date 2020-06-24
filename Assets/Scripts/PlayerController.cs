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

    [SerializeField] private KeyCode UpShootKey, RightShootKey, DownShootKey, LeftShootKey;

    [SerializeField] private Transform ShotOriginsN, ShotOriginsNE, ShotOriginsE, ShotOriginsSE, ShotOriginsS, ShotOriginsSW, ShotOriginsW, ShotOriginsNW;

    [SerializeField] private Transform ShotParent;

    [SerializeField] private float MovementSpeed = 2.0f;

    [SerializeField] private float ShootCooldown;
    private float ShootTimer = 0;


    void Start()
    {
        ShootTimer = 0;
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

        ShootTimer -= Time.deltaTime;

        if(ShootTimer > 0)
            return;

        if (Input.GetKey(UpShootKey) && Input.GetKey(RightShootKey)) Shoot(Direction.NORTH_EAST);
        else if (Input.GetKey(RightShootKey) && Input.GetKey(DownShootKey)) Shoot(Direction.SOUTH_EAST);
        else if (Input.GetKey(LeftShootKey) && Input.GetKey(DownShootKey)) Shoot(Direction.SOUTH_WEST);
        else if (Input.GetKey(LeftShootKey) && Input.GetKey(UpShootKey)) Shoot(Direction.NORTH_WEST);
        else if (Input.GetKey(UpShootKey)) Shoot(Direction.NORTH);
        else if (Input.GetKey(RightShootKey)) Shoot(Direction.EAST);
        else if (Input.GetKey(DownShootKey)) Shoot(Direction.SOUTH);
        else if (Input.GetKey(LeftShootKey)) Shoot(Direction.WEST);
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
        ShootTimer = ShootCooldown;
        switch (pDirection)
        {
            case Direction.NORTH:
                Instantiate(Shot, ShotOriginsN.position, ShotOriginsN.rotation, ShotParent);
                break;
            case Direction.NORTH_EAST:
                Instantiate(Shot, ShotOriginsNE.position, ShotOriginsNE.rotation, ShotParent);
                break;
            case Direction.EAST:
                Instantiate(Shot, ShotOriginsE.position, ShotOriginsE.rotation, ShotParent);
                break;
            case Direction.SOUTH_EAST:
                Instantiate(Shot, ShotOriginsSE.position, ShotOriginsSE.rotation, ShotParent);
                break;
            case Direction.SOUTH:
                Instantiate(Shot, ShotOriginsS.position, ShotOriginsS.rotation, ShotParent);
                break;
            case Direction.SOUTH_WEST:
                Instantiate(Shot, ShotOriginsSW.position, ShotOriginsSW.rotation, ShotParent);
                break;
            case Direction.WEST:
                Instantiate(Shot, ShotOriginsW.position, ShotOriginsW.rotation, ShotParent);
                break;
            case Direction.NORTH_WEST:
                Instantiate(Shot, ShotOriginsNW.position, ShotOriginsNW.rotation, ShotParent);
                break;
            
        }
    }
}
