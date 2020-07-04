using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Shot;
    [SerializeField] private GameObject SpreadShot;
    private GameObject CurrentAmmo;

    [SerializeField] public int HP;
    [SerializeField] public int Money;

    [SerializeField] private Transform ShotOriginsN, ShotOriginsNE, ShotOriginsE, ShotOriginsSE, ShotOriginsS, ShotOriginsSW, ShotOriginsW, ShotOriginsNW;

    [SerializeField] private Transform ShotParent;

    [SerializeField] private float MovementSpeed = 2.0f;

    [SerializeField] private float ShootCooldown;
    [SerializeField] private float ShootTimer = 0;

    [SerializeField] private int CurrentPowerup = 0; //CurrentPowerup 0=none, 1=spreadshot
    [SerializeField] private float PowerupDuration = 10;
    private float PowerupTimer = 0;

    [SerializeField] private PlayerChestBehaviour chest;
    [SerializeField] private Animator legs;
    
    private Vector2 moveAxis;
    private Vector2 shootAxis;
    private PlayerControls controls = null;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        legs.Play("pants stand still");
        CurrentAmmo = Shot;
        ShootTimer = 0;
        if(Shot == null)
        {
            Debug.LogError("Player Shot prefab not linked");
        }
    }


    void Update()
    {
        Move2D();

        ShootTimer -= Time.deltaTime;
        PowerupTimer -= Time.deltaTime;

        if (PowerupTimer <= 0)
            CurrentPowerup = 0;

        if(ShootTimer > 0)
            return;

        Shoot2D();

   }

    public void HandleMoveControls()
    {
        moveAxis = controls.Player.Move.ReadValue<Vector2>();
        Debug.Log(moveAxis);
    }

    public void HandleShootControls()
    {
        shootAxis = controls.Player.Shoot.ReadValue<Vector2>();
    }


    private void Move2D()
    {
        if (moveAxis.y > 0.2f) 
        {
            if(moveAxis.x > 0.2f) //north east
            {
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants up right");
                legsLeftRight(0);
            }
            else if(moveAxis.x < -0.2f) //north west
            {
                transform.position += Vector3.up * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants up right");
                legsLeftRight(180);
            }
            else //north
            {
                transform.position += Vector3.up * MovementSpeed * Time.deltaTime;
                legs.Play("pants up");
            }
        }
        else if(moveAxis.y < -0.2f)
        {
            if (moveAxis.x > 0.2f) //south east
            {
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.right * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants down right");
                legsLeftRight(0);
            }
            else if (moveAxis.x < -0.2f) //south west
            {
                transform.position += Vector3.down * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                transform.position += Vector3.left * Mathf.Sqrt(2) / 2 * MovementSpeed * Time.deltaTime;
                legs.Play("pants down right");
                legsLeftRight(180);
            }
            else //south
            {
                transform.position += Vector3.down * MovementSpeed * Time.deltaTime;
                legs.Play("pants down");
            }
        }
        else
        {
            if (moveAxis.x > 0.2f) //east
            {
                transform.position += Vector3.right * MovementSpeed * Time.deltaTime;
                legs.Play("pants right");
                legsLeftRight(0);
            }
            else if (moveAxis.x < -0.2f) //west
            {
                transform.position += Vector3.left * MovementSpeed * Time.deltaTime;
                legs.Play("pants right");
                legsLeftRight(180);
            }
            else
                legs.Play("pants stand still");

        }
    }




    private void Shoot2D()
    {
        if (PowerupTimer > 0 && CurrentPowerup == 1)
            CurrentAmmo = SpreadShot;
        else
            CurrentAmmo = Shot;

        ShootTimer = ShootCooldown;

        if (shootAxis.y > 0.2f)
        {
            if (shootAxis.x > 0.2f) //north east
            {
                Instantiate(CurrentAmmo, ShotOriginsNE.position, ShotOriginsNE.rotation, ShotParent);
                chest.face(Direction.NORTH_EAST);
            }
            else if (shootAxis.x < -0.2f) //north west
            {
                Instantiate(CurrentAmmo, ShotOriginsNW.position, ShotOriginsNW.rotation, ShotParent);
                chest.face(Direction.NORTH_WEST);
            }
            else //north
            {
                Instantiate(CurrentAmmo, ShotOriginsN.position, ShotOriginsN.rotation, ShotParent);
                chest.face(Direction.NORTH);
            }
        }
        else if (shootAxis.y < -0.2f)
        {
            if (shootAxis.x > 0.2f) //south east
            {
                Instantiate(CurrentAmmo, ShotOriginsSE.position, ShotOriginsSE.rotation, ShotParent);
                chest.face(Direction.SOUTH_EAST);
            }
            else if (shootAxis.x < -0.2f) //south west
            {
                Instantiate(CurrentAmmo, ShotOriginsSW.position, ShotOriginsSW.rotation, ShotParent);
                chest.face(Direction.SOUTH_WEST);
            }
            else //south
            {
                Instantiate(CurrentAmmo, ShotOriginsS.position, ShotOriginsS.rotation, ShotParent);
                chest.face(Direction.SOUTH);
            }
        }
        else
        {
            if (shootAxis.x > 0.2f) //east
            {
                Instantiate(CurrentAmmo, ShotOriginsE.position, ShotOriginsE.rotation, ShotParent);
                chest.face(Direction.EAST);
            }
            else if (shootAxis.x < -0.2f) //west
            {
                Instantiate(CurrentAmmo, ShotOriginsW.position, ShotOriginsW.rotation, ShotParent);
                chest.face(Direction.WEST);
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //powerups
        if (collision.CompareTag("Powerup"))
        {
            CurrentPowerup = 1;
            PowerupTimer = PowerupDuration;
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("Money"))
        {
            Money++;
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
