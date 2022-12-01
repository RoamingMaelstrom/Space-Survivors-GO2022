using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class TogglePause : MonoBehaviour
{
    [SerializeField] BoolSOEvent pauseEvent;
    public bool statePaused = false;


    public void TogglePauseUnpause(){
        if (statePaused)
        {
            statePaused = false;
            pauseEvent.Invoke(false);
            return;
        }

        statePaused = true;
        pauseEvent.Invoke(true);
    }

}
