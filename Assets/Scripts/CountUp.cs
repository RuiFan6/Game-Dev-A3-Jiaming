using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUp : MonoBehaviour
{
    public Text timerText;
    public float thirds;
    public float seconds;
    public float minutes;
    void Update()
    {
        if (CountDown.pause)
        {
            timerText.text = "00:00:00";
        }
        else
        {
            thirds = (int)(((Time.time-3f)*60f)%60f);
            seconds = (int)((Time.time-3)%60f);
            minutes = (int)((Time.time-3)/60f);
            timerText.text = "Time" + "\n" + minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + thirds.ToString("00");
        }  
    }
}
