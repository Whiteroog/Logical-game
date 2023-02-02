using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopWatch : MonoBehaviour
{
    private float _seconds = 0;

    public UnityEvent<int> passedSecond;
    
    void Update()
    {
        _seconds += Time.deltaTime;
        passedSecond.Invoke(Convert.ToInt32(Math.Floor(_seconds)));
    }
}
