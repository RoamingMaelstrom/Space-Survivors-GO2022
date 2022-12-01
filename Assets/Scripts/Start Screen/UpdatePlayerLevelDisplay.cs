using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerLevelDisplay : MonoBehaviour
{
    [SerializeField] PlayerLevel playerLevelScript;
    [SerializeField] LoadOnStart loadOnStartScript;

    private void Start() 
    {
        playerLevelScript.currentLevel = loadOnStartScript.playerLevel;
        playerLevelScript.xp = loadOnStartScript.playerXp;
        if (playerLevelScript.levelBounds.Count == 0) return;
        playerLevelScript.xpProgressInCurrentLevel = playerLevelScript.CalculateXpProgressInCurrentLevel(playerLevelScript.currentLevel,
         (int)playerLevelScript.xp, playerLevelScript.levelBounds);
    }
}
