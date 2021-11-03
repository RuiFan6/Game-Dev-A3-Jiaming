using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private Tweener tweener;
    public Animator animator;
    public AudioSource m1;

    // Start is called before the first frame update
    void Start()
    {
        m1 = GetComponent<AudioSource>();
        tweener = gameObject.GetComponent<Tweener>();
        tweener.AddTween(item.transform, item.transform.position, item.transform.position, 0.0f);
        //StartCoroutine(RunCircle());
        //animator.SetInteger("move", 1);
        animator.SetInteger("move", 1);
    }

    //void Awake()
     //{
        //StartCoroutine(RunCircle());
     //}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {   
            //Debug.Log("A pressed");
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.0f,9.0f,0.0f), 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {   
            //Debug.Log("D pressed");
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-7.0f,13.0f,0.0f), 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {   
            //Debug.Log("W pressed");
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.0f,13.0f,0.0f), 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {   
            //Debug.Log("S pressed");
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-7.0f,9.0f,0.0f), 1.5f);
        }
    }

    IEnumerator RunCircle()
    {   
        while(true)
        {
            animator.SetInteger("move", 1);
            m1.Play();
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.0f,13.0f,0.0f), 1.5f);
            yield return new WaitForSeconds(2);
            animator.SetInteger("move", 2);
            m1.Play();
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-7.0f,13.0f,0.0f), 1.5f);
            yield return new WaitForSeconds(2);
            animator.SetInteger("move", 3);
            m1.Play();
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-7.0f,9.0f,0.0f), 1.5f);
            yield return new WaitForSeconds(2);
            animator.SetInteger("move", 4);
            m1.Play();
            tweener.AddTween(item.transform, item.transform.position, new Vector3(-12.0f,9.0f,0.0f), 1.5f);
            yield return new WaitForSeconds(2);
        }

    }
}