using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using System;

public class PauseManager : MonoBehaviour
{
    [SerializeField] BoolSOEvent pauseEvent;
    [SerializeField] SOEvent enterPauseEvent;
    [SerializeField] SOEvent exitPauseEvent;

    public int pauseCounter = 0;
    public bool isPaused = false;

    private void Awake() {
        pauseEvent.AddListener(UpdatePauseCounter);
    }

    private void UpdatePauseCounter(bool pauseRequest)
    {
        if (pauseRequest) pauseCounter ++;
        else pauseCounter --;
        UpdatePauseState();
    }

    private void UpdatePauseState()
    {
        if (pauseCounter > 0 && !isPaused)
        {
            enterPauseEvent.Invoke();
            isPaused = true;
            Time.timeScale = 0;
        }
        if (pauseCounter == 0 && isPaused) 
        {
            isPaused = false;
            Time.timeScale = 1;
            exitPauseEvent.Invoke();
        }
    }
}
