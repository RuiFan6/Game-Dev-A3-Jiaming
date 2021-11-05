using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    private int count = 3;
    public Text countDownText;
    public static bool pause;
    // Start is called before the first frame update
    void Start()
    {
        pause = true;
        StartCoroutine(CountDownCountine());
    }

    void Update() 
    {
        if (pause)
        {
            PacStudentController.freeze = true;
        }
        else
        {   
            PacStudentController.freeze = false;
        }
    }

    IEnumerator CountDownCountine()
    {
        while (count >= 1)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count = count - 1;
        }
        countDownText.text = "GO ! ! !";
        pause = false;
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }
}
