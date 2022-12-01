
using UnityEngine;
using SOEvents;
using System;

public class OpenLevelUpScreen : MonoBehaviour
{
    [SerializeField] IntSOEvent playerLevelUpEvent;
    [SerializeField] LevelUpgradeSOEvent applyLevelUpgradeEvent;
    [SerializeField] BoolSOEvent pauseEvent;
    [SerializeField] GameObject content;
    [SerializeField] LevelUpgradePicker levelUpgradePicker;
    [SerializeField] LevelUpgrade[] levelUpgrades = new LevelUpgrade[3];
    [SerializeField] LevelUpTabLogic[] upgradeTabs = new LevelUpTabLogic[3];

    private void Awake() {
        playerLevelUpEvent.AddListener(EnablePanel);
        applyLevelUpgradeEvent.AddListener(DisablePanel);
    }

    private void EnablePanel(int playerLevel)
    {
        content.SetActive(true);
        levelUpgrades = levelUpgradePicker.GetThreeRandomLevelUpgrades();
        for (int i = 0; i < 3; i++)
        {
            upgradeTabs[i].Setup(levelUpgrades[i]);
        }
        pauseEvent.Invoke(true);
    }

    private void DisablePanel(LevelUpgrade arg0)
    {
        content.SetActive(false);
        pauseEvent.Invoke(false);
    }
}
