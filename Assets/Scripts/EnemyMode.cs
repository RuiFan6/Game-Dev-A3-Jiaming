using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMode : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Animator eAnimator;
    [SerializeField] private GameObject item1;
    [SerializeField] private Animator eAnimator1;
    [SerializeField] private GameObject item2;
    [SerializeField] private Animator eAnimator2;
    [SerializeField] private GameObject item3;
    [SerializeField] private Animator eAnimator3;

    public static bool isScared;

    public bool enemyDie;

    public float timer;
    public float dTimer = 5f;

    void Start()
    {
        enemyDie = false;
        isScared = false;
        timer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f && isScared)
        {
            timer = timer - Time.deltaTime;
        }

        isScared = PlayerCollision.scared;
        if(isScared)
        {
            //BeScared();
            if(enemyDie == false)
            {
                BeScared();
            }
            else
            {
                EnemyDie();
            }
        }
        else if(isScared == false)
        {
            timer = 10f;
        }
        //Debug.Log(timer);
        //Debug.Log(isScared);
        if (enemyDie)
        {
            eAnimator.SetInteger("eMove", 7);
            eAnimator1.SetInteger("eMove", 7);
            eAnimator2.SetInteger("eMove", 7);
            eAnimator3.SetInteger("eMove", 7);
        }
    }

    void BeScaredcc()
    {
        eAnimator.SetInteger("eMove", 5);
        eAnimator1.SetInteger("eMove", 5);
        eAnimator2.SetInteger("eMove", 5);
        eAnimator3.SetInteger("eMove", 5);
        if (timer <= 3f && timer > 0f)
        {
            eAnimator.SetInteger("eMove", 6);
            eAnimator1.SetInteger("eMove", 6);
            eAnimator2.SetInteger("eMove", 6);
            eAnimator3.SetInteger("eMove", 6);
        }
        else if (timer <= 0f)
        {
            eAnimator.SetInteger("eMove", 3);
            eAnimator1.SetInteger("eMove", 3);
            eAnimator2.SetInteger("eMove", 3);
            eAnimator3.SetInteger("eMove", 3);
            isScared = false;
        }
    }

    void BeScared()
    {
        if (Enemy.contact)
        {
            eAnimator.SetInteger("eMove", 7);
            eAnimator1.SetInteger("eMove", 7);
            eAnimator2.SetInteger("eMove", 7);
            eAnimator3.SetInteger("eMove", 7);
        }
        else
        {
            eAnimator.SetInteger("eMove", 5);
            eAnimator1.SetInteger("eMove", 5);
            eAnimator2.SetInteger("eMove", 5);
            eAnimator3.SetInteger("eMove", 5);
            if (timer <= 3f && timer > 0f)
            {
                eAnimator.SetInteger("eMove", 6);
                eAnimator1.SetInteger("eMove", 6);
                eAnimator2.SetInteger("eMove", 6);
                eAnimator3.SetInteger("eMove", 6);
            }
            else if (timer <= 0f)
            {
                eAnimator.SetInteger("eMove", 3);
                eAnimator1.SetInteger("eMove", 3);
                eAnimator2.SetInteger("eMove", 3);
                eAnimator3.SetInteger("eMove", 3);
                isScared = false;
            }
        }
    }

    void EnemyDie()
    {
        if (dTimer > 0)
        {
            dTimer = dTimer - Time.deltaTime;
        }
        else
        {
            enemyDie = false;
            dTimer = 5f;
        }
    }

    

}
