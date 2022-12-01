using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Call when player starts application (e.g. enters Start Menu).
public class LoadOnStart : MonoBehaviour
{
    public int playerLevel;
    public float playerXp;
    public float maxHpMod;
    public float damageTakenMod;
    public float moveSpeedMod;
    public float damageMod;
    public float attackSpeedMod;
    public float dropProbabilityMod;
    public float xpGainMod;
    public float deliveryDistanceMod;
    public float deliveryPayoutMod;

    private void Awake() 
    {
        RunSetupSequence();
    }

    public void RunSetupSequence()
    {
        Time.timeScale = 1;
        if (!PlayerPrefs.HasKey("_USERDATA_EXISTS")) FirstTimeSetupUserData();
        LoadUserDataForMainMenu();
    }

    private void FirstTimeSetupUserData()
    {
        Debug.Log("Performing first time setup");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("_USERDATA_EXISTS", 1);
        PlayerPrefs.SetInt("hintNum", 0);

        PlayerPrefs.SetInt("playerLevel", 1);
        PlayerPrefs.SetFloat("playerXp", 0);

        PlayerPrefs.SetFloat("maxHpMod", 1);
        PlayerPrefs.SetFloat("damageTakenMod", 1);
        PlayerPrefs.SetFloat("moveSpeedMod", 1);

        PlayerPrefs.SetFloat("damageMod", 1);
        PlayerPrefs.SetFloat("attackSpeedMod", 1);
        PlayerPrefs.SetFloat("dropProbabilityMod", 1);

        PlayerPrefs.SetFloat("xpGainMod", 1);
        PlayerPrefs.SetFloat("deliveryDistanceMod", 1);
        PlayerPrefs.SetFloat("deliveryPayoutMod", 1);

        // These are currently only here as an example of what the system might look like. Not in Use.
        PlayerPrefs.SetInt("unlockedShip1", 0);
        PlayerPrefs.SetInt("unlockedShip2", 0);
        PlayerPrefs.SetInt("unlockedShip3", 0);

        // These are currently only here as an example of what the system might look like. Not in Use.
        PlayerPrefs.SetInt("unlockedItem1", 0);
        PlayerPrefs.SetInt("unlockedItem2", 0);
        PlayerPrefs.SetInt("unlockedItem3", 0);

        PlayerPrefs.Save();
    }

    private void LoadUserDataForMainMenu()
    {
        playerLevel = PlayerPrefs.GetInt("playerLevel");
        playerXp = PlayerPrefs.GetFloat("playerXp");

        maxHpMod = PlayerPrefs.GetFloat("maxHpMod");
        damageTakenMod = PlayerPrefs.GetFloat("damageTakenMod");
        moveSpeedMod = PlayerPrefs.GetFloat("moveSpeedMod");

        damageMod = PlayerPrefs.GetFloat("damageMod");
        attackSpeedMod = PlayerPrefs.GetFloat("attackSpeedMod");
        dropProbabilityMod = PlayerPrefs.GetFloat("dropProbabilityMod");
        
        xpGainMod = PlayerPrefs.GetFloat("xpGainMod");
        deliveryDistanceMod = PlayerPrefs.GetFloat("deliveryDistanceMod");
        deliveryPayoutMod = PlayerPrefs.GetFloat("deliveryPayoutMod");
        
    }
}
