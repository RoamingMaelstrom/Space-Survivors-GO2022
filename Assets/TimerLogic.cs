using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLogic : MonoBehaviour
{
    public float timePlayed;

    private void FixedUpdate() {
        timePlayed += Time.fixedDeltaTime;
    }
}
