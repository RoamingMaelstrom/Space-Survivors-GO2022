using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveAfterLevelling : MonoBehaviour
{
    public void SavePersistentModData(PersistentModifiers mods)
    {
        PlayerPrefs.SetFloat("maxHpMod", mods.maxHpMod);
        PlayerPrefs.SetFloat("damageMod", mods.damageMod);
        PlayerPrefs.SetFloat("damageTakenMod", mods.damageTakenMod);
        PlayerPrefs.SetFloat("attackSpeedMod", mods.attackSpeedMod);
        PlayerPrefs.SetFloat("dropProbabilityMod", mods.dropProbabilityMod);
        PlayerPrefs.SetFloat("deliveryDistanceMod", mods.deliveryDistanceMod);
        PlayerPrefs.SetFloat("deliveryPayoutMod", mods.deliveryPayoutMod);
        PlayerPrefs.SetFloat("xpGainMod", mods.xpGainMod);
        PlayerPrefs.SetFloat("moveSpeedMod", mods.moveSpeedMod);

        PlayerPrefs.Save();
    }

    public void SavePlayerLevelData(PlayerLevel playerLevelScript)
    {
        PlayerPrefs.SetInt("playerLevel", playerLevelScript.currentLevel);
        PlayerPrefs.SetFloat("playerXp", playerLevelScript.xp);

        PlayerPrefs.Save();
    }
}
