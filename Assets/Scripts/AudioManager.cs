using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public AudioClip c1;
    public AudioClip c2;
    public AudioSource s1;
    //public AudioClip playerMoving;
    // Start is called before the first frame update
    void Start()
    {
        s1 = GetComponent<AudioSource>();
        s1.clip = c1;
    }

    // Update is called once per frame
    void Update()
    {
        if (s1.clip == c1 && EnemyMode.isScared)
        {
            s1.clip = c2;
            s1.Play();
        }
        if (s1.clip == c2 && EnemyMode.isScared == false)
        {
            s1.clip = c1;
            s1.Play();
        }
        if (CountDown.pause)
        {
            s1.Pause();
        }
        else
        {
            s1.UnPause();
            //Debug.Log("Play");
        }
    }
}
