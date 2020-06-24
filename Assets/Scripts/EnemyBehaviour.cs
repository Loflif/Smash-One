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

        if (attackVector.magnitude > 1 && !attacking)
            Move(attackVector);

        else if (attackVector.magnitude <= 1 && !attacking)
        {
            attacking = true;
            StartCoroutine(Attack(attackVector));
        }
    }

    void Move(Vector3 direction)
    {
        direction.Normalize();
        direction *= speed;

        transform.position += direction * Time.deltaTime;
    }

    //I don't know what an IEnumerator is, but it seems to let me do this waitforseconds thing that delays the damage until the animation is done I guess
    IEnumerator Attack(Vector3 direction)
    {
        anim.Play("Base Layer.attack");
        yield return new WaitForSeconds(1);
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if(distanceToPlayer.magnitude < 1.25f)
        {
            //do damage to player
            Debug.Log("damage");
        }
        attacking = false;
        anim.Play("Base Layer.walk");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "PlayerShot")
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }
}
