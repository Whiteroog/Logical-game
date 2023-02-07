using System;
using UnityEngine;
using UnityEngine.Events;

namespace ThisProject.GameObjects.Stopwatch
{
    public class Stopwatch : MonoBehaviour
    {
        public UnityEvent<string> updateStopwatchIndicator;

        public bool ActiveStopwatch { set; get; }  = true;
    
        private float _seconds = 0;
        
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
