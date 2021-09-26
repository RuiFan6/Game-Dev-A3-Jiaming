using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log("activetween---endpos" + activeTween.EndPos);
        float distance = Vector3.Distance(activeTween.Target.position, activeTween.EndPos);
        if (distance > 0.01f)
        {
            float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;
            Vector3 currentPos = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            activeTween.Target.position = currentPos;
        }
        else
        {
            activeTween.Target.position = activeTween.EndPos;
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
    }

}