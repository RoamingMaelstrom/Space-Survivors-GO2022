using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using System;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] FloatSOEvent updatePlayerMoneyEvent;
    [SerializeField] IntSOEvent playerLevelUpEvent;
    [SerializeField] GameModifiers gameModifiers;
    [SerializeField] PersistentModifiers persistentModifiers;
    public int currentLevel = 1;
    public float xp = 0;
    public float xpProgressInCurrentLevel = 0;
    public List<int> levelBounds = new List<int>();


    private void Awake() {
        updatePlayerMoneyEvent.AddListener(GainXpFromMoney);
    }

    public void GainXpFromMoney(float value)
    {
        if (value <= 0) return;
        float valueToAdd = (value + gameModifiers.GetAdditionValueOfType(UpgradeType.XP_GAIN_CURRENT_GAME)) 
         * gameModifiers.GetMultiplicationValueOfType(UpgradeType.XP_GAIN_CURRENT_GAME) * persistentModifiers.xpGainMod;
        xp += valueToAdd;
        xpProgressInCurrentLevel += valueToAdd;
        if (xpProgressInCurrentLevel > levelBounds[currentLevel - 1]) LevelUp();
    }

    public int CalculateXpProgressInCurrentLevel(int currentLevel, int xp, List<int> levelBounds)
    {
        int totalXpPastLevels = 0;
        for (int i = 0; i < currentLevel - 1; i++)
        {
            totalXpPastLevels += levelBounds[i];   
        }

        return xp - totalXpPastLevels;
    }

    public void LevelUp()
    {
        xpProgressInCurrentLevel -= levelBounds[currentLevel - 1];
        currentLevel++;
        if (currentLevel > levelBounds.Count - 3) AutoGenerateLevelBound(levelBounds);
        playerLevelUpEvent.Invoke(currentLevel);
    }

    public void AutoGenerateLevelBound(List<int> levelBounds)
    {
        levelBounds.Add((int)(levelBounds[levelBounds.Count - 1] + 250));
    }
}
