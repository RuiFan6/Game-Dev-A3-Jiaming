using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private Tweener tweener;
    public Animator animator;
    public AudioSource m1;
    private Vector3 destination;
    private Vector3 moveDir;
    private int lastInput;
    private bool walkable;

    void Start()
    {
        m1 = GetComponent<AudioSource>();
        tweener = gameObject.GetComponent<Tweener>();
        tweener.AddTween(item.transform, item.transform.position, item.transform.position, 0.0f);
        animator.SetInteger("move", 3);
        walkable = true;
    }

    void Update()
    {
       GetKeyInput();
       MoveOperator();
    }

    void GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {   
            lastInput = 4;
            moveDir = new Vector3(-1f, 0f, 0f);
            Moveit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {   
            lastInput = 2;
            moveDir = new Vector3(1f, 0f, 0f);
            Moveit();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {   
            lastInput = 1;
            moveDir = new Vector3(0f, 1f, 0f);
            Moveit();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {   
            lastInput = 3;
            moveDir = new Vector3(0f, -1f, 0f);
            Moveit();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {   
            walkable = false;
        }
    }
    void MoveOperator()
    {
        if (destination == item.transform.position)
        {
            if (walkable)
            {
                Moveit();
            }
            else
            {
                StopMoving();
            }
        }
    }

    void Moveit()
    {
        walkable = true;
        destination = item.transform.position + moveDir;
        animator.enabled = true;
        animator.SetInteger("move", lastInput);
        m1.Play();
        tweener.AddTween(item.transform, item.transform.position, destination, 0.5f);
    }

    void StopMoving()
    {
        animator.enabled = false;
    }
}