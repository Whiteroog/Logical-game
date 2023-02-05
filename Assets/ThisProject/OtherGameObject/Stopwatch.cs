using System;
using UnityEngine;
using UnityEngine.Events;

namespace ThisProject.OtherGameObject
{
    public class Stopwatch : MonoBehaviour
    {
        public UnityEvent<string> updateStopwatchIndicator;

        public bool ActiveStopwatch { set; get; }  = true;
    
        public float _seconds = 0;
        
        public float GetSeconds() => _seconds;

        void Update()
        {
            if(ActiveStopwatch)
            {
                _seconds += Time.deltaTime;

                int convertedSecondToDecimal = Convert.ToInt32(Math.Floor(_seconds));

                updateStopwatchIndicator.Invoke($"{convertedSecondToDecimal / 60:d2}:{convertedSecondToDecimal % 60:d2}");
            }
        }
    }
}
