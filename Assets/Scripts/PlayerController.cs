using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Shot;
    [SerializeField] private GameObject SpreadShot;
    private GameObject CurrentAmmo;

    [SerializeField] public int HP;

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

    [SerializeField] private int CurrentPowerup = 0; //CurrentPowerup 0=none, 1=spreadshot
    [SerializeField] private float PowerupDuration = 10;
    private float PowerupTimer = 0;

    [SerializeField] private PlayerChestBehaviour chest;
    [SerializeField] private Animator legs;



    void Start()
    {
        legs.Play("pants down");
        CurrentAmmo = Shot;
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
        PowerupTimer -= Time.deltaTime;

        if (PowerupTimer <= 0)
            CurrentPowerup = 0;

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
                legs.Play("pants up");
                break;
            case Direction.NORTH_EAST:
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants up right");
                legsLeftRight(0);
                break;
            case Direction.EAST:
                transform.position += Vector3.right * MovementSpeed * Time.deltaTime;
                legs.Play("pants right");
                legsLeftRight(0);
                break;
            case Direction.SOUTH_EAST:
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants down right");
                legsLeftRight(0);
                break;
            case Direction.SOUTH:
                transform.position += Vector3.down * MovementSpeed * Time.deltaTime;
                legs.Play("pants down");
                break;
            case Direction.SOUTH_WEST:
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants down right");
                legsLeftRight(180);
                break;
            case Direction.WEST:
                transform.position += Vector3.left * MovementSpeed * Time.deltaTime;
                legs.Play("pants right");
                legsLeftRight(180);
                break;
            case Direction.NORTH_WEST:
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants up right");
                legsLeftRight(180);
                break;
        }
    }

    private void Shoot(Direction pDirection)
    {
        if (PowerupTimer > 0 && CurrentPowerup == 1)
            CurrentAmmo = SpreadShot;
        else
            CurrentAmmo = Shot;

        ShootTimer = ShootCooldown;
        switch (pDirection)
        {
            case Direction.NORTH:
                Instantiate(CurrentAmmo, ShotOriginsN.position, ShotOriginsN.rotation, ShotParent);
                chest.face(0);
                break;
            case Direction.NORTH_EAST:
                Instantiate(CurrentAmmo, ShotOriginsNE.position, ShotOriginsNE.rotation, ShotParent);
                chest.face(1);
                break;
            case Direction.EAST:
                Instantiate(CurrentAmmo, ShotOriginsE.position, ShotOriginsE.rotation, ShotParent);
                chest.face(2);
                break;
            case Direction.SOUTH_EAST:
                Instantiate(CurrentAmmo, ShotOriginsSE.position, ShotOriginsSE.rotation, ShotParent);
                chest.face(3);
                break;
            case Direction.SOUTH:
                Instantiate(CurrentAmmo, ShotOriginsS.position, ShotOriginsS.rotation, ShotParent);
                chest.face(4);
                break;
            case Direction.SOUTH_WEST:
                Instantiate(CurrentAmmo, ShotOriginsSW.position, ShotOriginsSW.rotation, ShotParent);
                chest.face(5);
                break;
            case Direction.WEST:
                Instantiate(CurrentAmmo, ShotOriginsW.position, ShotOriginsW.rotation, ShotParent);
                chest.face(6);
                break;
            case Direction.NORTH_WEST:
                Instantiate(CurrentAmmo, ShotOriginsNW.position, ShotOriginsNW.rotation, ShotParent);
                chest.face(7);
                break;
            
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //powerups
        if (collision.gameObject.tag == "Powerup")
        {
            CurrentPowerup = 1;
            PowerupTimer = PowerupDuration;
            Destroy(collision.gameObject);
        }
    }

    //180 to face legs right. 0 to face left
    void legsLeftRight(int direction)
    {
        GameObject legsGO = legs.transform.gameObject;
        legsGO.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            direction,
            gameObject.transform.eulerAngles.z);
    }

    public void takeDamage()
    {
        HP--;
        if(HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
