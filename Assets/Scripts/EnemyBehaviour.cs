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
    private Animator anim;
    private bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //REMOVE THIS SHIT
        player = GameObject.Find("playerONE");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 attackVector = player.transform.position - transform.position;

        if (attackVector.x > 0)
            Face(180);
        else
            Face(0);

        if (attackVector.magnitude > 1 && !attacking)
            Move(attackVector);

        else if (attackVector.magnitude <= 1 && !attacking)
        {
            attacking = true;
            anim.Play("Base Layer.attack");
        }
    }

    void Face(float direction)
    {
        gameObject.transform.eulerAngles = new Vector3(
            gameObject.transform.eulerAngles.x,
            direction,
            gameObject.transform.eulerAngles.z);
    }

    void Move(Vector3 direction)
    {
        anim.Play("Base Layer.walk");
        direction.Normalize();
        direction *= speed;

        transform.position += direction * Time.deltaTime;
    }


    //triggered by animation event 
    private void DoDamage()
    {
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if (distanceToPlayer.magnitude < 1.25f)
        {
            //do damage to player
            Debug.Log("damage");
            player.GetComponent<PlayerController>().takeDamage();
        }
    }

    //triggered by animation event
    private void EndAttack()
    {
        anim.Play("Base Layer.idle");
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "PlayerShot")
        {
            Destroy(collider.gameObject);
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

}
