using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public int score;
    public Text text;
    public static bool scared;
    public static bool slay;
    public static bool death;
    public Animator playerAnimator;
    public float deathTimer = 5f;
    public float upTimer = 10f;
    public AudioClip moveSound;
    public AudioClip eatSound;
    public AudioClip dieSound;

    

    void Start()
    {
        scared = false;
        slay = false;
        death = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "Score: " + score;
        if (Input.GetKeyDown(KeyCode.K))
        {   
            scared = true;
            //ModeControl();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {   
            scared = false;
        //    ModeSwitch();
        }



        //ModeSwitch();
        //scared = EnemyMode.isScared;
        //Debug.Log("scared   " + scared);
        //Debug.Log("revert" + revert);
        //Debug.Log(upTimer);
        //Debug.Log(deathTimer);

        if(scared)
        {
            Upgrade();
        }

        if(death)
        {
            Die();
        }
        
        ModeControl();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Walls")
        {
            //Debug.Log("Walls");
        }
        if (other.gameObject.tag == "Pellets")
        {
            score = score + 10;
            EatSound();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "PowerPellets")
        {
            EatSound();
            other.gameObject.SetActive(false);
            scared = true;
        }
        if (other.gameObject.tag == "Enemies")
        {
            if (scared == false)
            {
                death = true;
            }
            else
            {
                slay = true;
            }
        }
        if (other.gameObject.tag == "Cherry")
        {
            other.gameObject.SetActive(false);
            score = score + 100;
            EatSound();
        }
    }

    private void EatSound()
    {
        GameObject pcl = GameObject.Find("PlayerControl");
        PacStudentController pslc = pcl.GetComponent<PacStudentController>();
        pslc.m1.PlayOneShot(eatSound, 0.2f);
    }

    private void ModeControl()
    {
        GameObject pc = GameObject.Find("PlayerControl");
        PacStudentController psc = pc.GetComponent<PacStudentController>();
        
        if (scared)
        {
            psc.animator.SetInteger("move", 5);
        }

        else
        {
            if (psc.animator.GetInteger("move") == 5)
            {
                psc.animator.SetInteger("move", 3);
            }
            if (death)
            {
                psc.animator.SetInteger("move", 7);
            }
            else if(death == false)
            {
                if (psc.animator.GetInteger("move") == 7)
                {
                    psc.animator.SetInteger("move", 3);
                }
            }
        }
    }

    void Upgrade()
    {
        scared = true;
        if (upTimer > 0)
        {
            upTimer = upTimer - Time.deltaTime;
        }
        else
        {
            scared = false;
            upTimer = 10f;
        }
    }
    void Die()
    {
        death = true;
        if (deathTimer > 0)
        {
            deathTimer = deathTimer - Time.deltaTime;
        }
        else
        {
            death = false;
            deathTimer = 5f;
        }
    }

}
