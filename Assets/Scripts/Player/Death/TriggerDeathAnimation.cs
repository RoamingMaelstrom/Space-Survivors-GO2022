using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using System;

public class TriggerDeathAnimation : MonoBehaviour
{
    [SerializeField] GameObjectBoolSOEvent playerDeathEvent;

    private void Awake() {
        playerDeathEvent.AddListener(TriggerFadeOutAnimation);
    }

    private void TriggerFadeOutAnimation(GameObject playerParent, bool arg1)
    {
        playerParent.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StartDeathAnimation");
    }
}
