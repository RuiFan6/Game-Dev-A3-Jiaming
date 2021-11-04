using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherry;
    public GameObject clone;
    private Tweener tweener;
    public Animator animator;
    public bool ready;
    private Vector3 thePos;

    void Start()
    {
        tweener = gameObject.GetComponent<Tweener>();
        tweener.AddTween(cherry.transform, cherry.transform.position, cherry.transform.position, 0.0f);
        ready = true;
    }
    void Update() 
    {
        if (clone == null)
        {
            if (ready == true)
            {   
                ready = false;
                Launch();
            }
        }
        else if (clone != null)
        {
            if(clone.transform.position == -thePos)
            {
                Destroy(clone);
                StartCoroutine(Wait());
            }   
        }
    }
    Vector3 RandomPos()
    {
        Vector3 pos = new Vector3(0f,0f,0f);
        int side = Random.Range(1, 5);
        if (side == 1)
        {
            pos = new Vector3 (Random.Range(-22f, -21f), Random.Range(-18f, 18f), 0f);
        }
        else if (side == 2)
        {
            pos = new Vector3 (Random.Range(21f, 22f), Random.Range(-18f, 18f), 0f);
        }
        else if (side == 3)
        {
            pos = new Vector3 (Random.Range(-21f, 21f), Random.Range(17f, 18f), 0f);
        }
        else if (side == 4)
        {
            pos = new Vector3 (Random.Range(-21f, 21f), Random.Range(-18f, -17f), 0f);
        }
        return pos;
    }

    void Launch()
    {
        thePos = RandomPos();
        clone = Instantiate(cherry, thePos, Quaternion.identity);
        tweener.AddTween(clone.transform, thePos, -thePos, 10f);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10);
        ready = true;
    }
}