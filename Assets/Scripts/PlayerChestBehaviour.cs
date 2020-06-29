using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChestBehaviour : MonoBehaviour
{
    SpriteRenderer facing;
    SpriteRenderer[] directions;

    // Start is called before the first frame update
    void Start()
    {
        facing = transform.GetChild(4).GetComponent<SpriteRenderer>();
        directions = new SpriteRenderer[transform.childCount];
        for(int i = 0; i<transform.childCount; i++)
        {
            directions[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void face(int direction)
    {
        facing.enabled = false;
        facing = directions[direction];
        facing.enabled = true;
    }

}
