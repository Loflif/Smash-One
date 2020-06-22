using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private int hp;

    // Start is called before the first frame update
    void Start()
    {
        //REMOVE THIS SHIT
        player = GameObject.Find("playerONE");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 attackVector = player.transform.position - transform.position;

        if (attackVector.magnitude > 1)
            Move(attackVector);

        else
            Attack(attackVector);
    }

    void Move(Vector3 direction)
    {
        direction.Normalize();
        direction *= speed;

        transform.position += direction * Time.deltaTime;
    }

    void Attack(Vector3 direction)
    {
        Thread.Sleep(500);
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if(distanceToPlayer.magnitude < 1.25f)
        {
            //do damage to player
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerShot")
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
