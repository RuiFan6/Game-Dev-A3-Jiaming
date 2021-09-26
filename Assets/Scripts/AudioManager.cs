using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public AudioClip c1;
    public AudioSource s1;
    // Start is called before the first frame update
    void Start()
    {
        s1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!s1.isPlaying)
        {
            s1.clip = c1;
            s1.Play();
        }
    }
}
