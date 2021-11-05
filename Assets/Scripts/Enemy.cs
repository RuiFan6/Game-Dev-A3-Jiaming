using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
    private const float speed = 10f;
    public static bool contact = false;
    private void Update() 
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        float x = 0f;
        float y = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            y = +1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y = -1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = +1f;
        }

        Vector3 moveDir = new Vector3(x,y).normalized;
        transform.position += moveDir*speed*Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(EnemyMode.isScared)
        {
            //Debug.Log("I died");
            contact = true;
        }
        else
        {
            //Debug.Log("kill ya");
            contact = false;
        }

    }

}
