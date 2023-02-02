using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStopWatch : MonoBehaviour
{
    public TMP_Text stopWatchText;

    public void UpdateStopWatchText(int seconds)
    {
        stopWatchText.text = $"{seconds / 60:d2}:{seconds % 60:d2}";
    }
}
