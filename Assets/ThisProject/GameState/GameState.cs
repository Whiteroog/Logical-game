using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public UnityEvent<bool> callGameMenu;

    public bool isMenuActive = false;

    private void Start()
    {
        callGameMenu.Invoke(isMenuActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            callGameMenu.Invoke(isMenuActive);
        }
    }
}
