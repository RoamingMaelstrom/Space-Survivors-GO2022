using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOnGameStart : MonoBehaviour
{    
    [SerializeField] PlayerLevel playerLevelData;
    [SerializeField] PersistentModifiers playerModsData;

    private void Start() {
        LoadIntoPlayerLevelScript(playerLevelData);
        LoadIntoPlayerPersistantModifiersScript(playerModsData);
    }

    private void LoadIntoPlayerLevelScript(PlayerLevel container)
    {
        container.currentLevel = PlayerPrefs.GetInt("playerLevel");
        container.xp = PlayerPrefs.GetFloat("playerXp");

        while (container.currentLevel > container.levelBounds.Count - 3)
        {
            container.AutoGenerateLevelBound(container.levelBounds);
        }

        container.xpProgressInCurrentLevel = container.CalculateXpProgressInCurrentLevel(container.currentLevel, (int)container.xp,
         container.levelBounds);
    }

    private void LoadIntoPlayerPersistantModifiersScript(PersistentModifiers container)
    {
        container.maxHpMod = PlayerPrefs.GetFloat("maxHpMod");
        container.damageTakenMod = PlayerPrefs.GetFloat("damageTakenMod");
        container.moveSpeedMod = PlayerPrefs.GetFloat("moveSpeedMod");
        container.damageMod = PlayerPrefs.GetFloat("damageMod");
        container.attackSpeedMod = PlayerPrefs.GetFloat("attackSpeedMod");
        container.dropProbabilityMod = PlayerPrefs.GetFloat("dropProbabilityMod");
        container.xpGainMod = PlayerPrefs.GetFloat("xpGainMod");
        container.deliveryDistanceMod = PlayerPrefs.GetFloat("deliveryDistanceMod");
        container.deliveryPayoutMod = PlayerPrefs.GetFloat("deliveryPayoutMod");
    }
}
