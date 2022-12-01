using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelNumberTextLogic : MonoBehaviour
{
    [SerializeField] PlayerLevel playerLevelScript;
    [SerializeField] TextMeshProUGUI levelText;


    private void FixedUpdate() {
        levelText.SetText(playerLevelScript.currentLevel.ToString());
    }
}
