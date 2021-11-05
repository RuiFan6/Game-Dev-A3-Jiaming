using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private Tweener tweener;
    public Animator animator;
    public AudioSource m1;
    public static bool freeze;
    private Vector3 destination;
    private int lastInput;
    private int currentInput;

    void Start()
    {
        freeze = false;
        m1 = GetComponent<AudioSource>();
        tweener = gameObject.GetComponent<Tweener>();
        animator.SetInteger("move", 3);
        animator.enabled = false;
        item.transform.position = new Vector3(-12f, 13f, 0);
    }

    void Update()
    {
        GetKeyInput();
        MoveOperator();
        Teleport();
        //Ohno();
    }

    void GetKeyInput()
    {
        if(freeze == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {   
                currentInput = 4;
                MoveOperator();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {   
                currentInput = 2;
                MoveOperator();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {   
                currentInput = 1;
                MoveOperator();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {   
                currentInput = 3;
                MoveOperator();
            }
        }
        
    }
    void MoveOperator()
    {
        if (destination == item.transform.position || animator.enabled == false)
        {
            if (MoveCheck(currentInput) == true)
            {
                lastInput = currentInput;
                Moveit(currentInput);
            }
            else if (MoveCheck(currentInput) == false && MoveCheck(lastInput) == true)
            {
                Moveit(lastInput);
            }
            else
            {
                StopMoving();
            }
        }
    }

    void Moveit(int inputToMove)
    {
        destination = InputToDir(inputToMove);
        animator.enabled = true;
        animator.SetInteger("move", inputToMove);
        m1.Play();
        tweener.AddTween(item.transform, item.transform.position, destination, 0.5f);
    }

    void StopMoving()
    {
        animator.enabled = false;
    }

    bool MoveCheck(int inputToCheck)
    {
        if (LevelGenerator.walkablePos.Contains(InputToDir(inputToCheck)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Vector3 InputToDir(int input)
    {
        Vector3 dir = new Vector3(0f,0f,0f);
        if(input == 1)
        {
            dir = new Vector3(0f, 1f, 0f);
        }
        else if(input == 2)
        {
            dir = new Vector3(1f, 0f, 0f);
        }
        else if(input == 3)
        {
            dir = new Vector3(0f, -1f, 0f);
        }
        else if(input == 4)
        {
            dir = new Vector3(-1f, 0f, 0f);
        }
        dir = dir + item.transform.position;
        return dir;
    }
    void Teleport()
    {
        if(item.transform.position == new Vector3(-13f, 0f, 0f))
        {
            item.transform.position = new Vector3(14f, 0f, 0f);
        }
        else if(item.transform.position == new Vector3(14f, 0f, 0f))
        {
            item.transform.position = new Vector3(-13f, 0f, 0f);
        }
    }

    void Ohno()
    {
        if (PlayerCollision.death)
        {
            animator.SetInteger("move", 7);
        }
        else
        {
            
        }
    }

}