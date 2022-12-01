using UnityEngine;
using TMPro;
using SOEvents;
using System;

public class LevelUpTabLogic : MonoBehaviour
{
    [SerializeField] LevelUpgradeSOEvent applyLevelUpgradeEvent;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    public LevelUpgrade currentLevelUpgrade;

    public void Setup(LevelUpgrade levelUpgrade)
    {
        nameText.SetText(GetLevelUpgradeNameText(levelUpgrade));
        descriptionText.SetText(GetLevelUpgradeDescriptionText(levelUpgrade));
        currentLevelUpgrade = levelUpgrade;
    }

    private string GetLevelUpgradeNameText(LevelUpgrade levelUpgrade)
    {
        if (levelUpgrade.upgradeType == LevelUpgradeType.MAX_HP) return "Ship Hull";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.MOVESPEED) return "Ship Thrusters";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.DAMAGE_TAKEN) return "Ship Armor";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.DAMAGE) return "Weapon Power";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.ATTACK_COOLDOWN) return "Attack Speed";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.DELIVERY_DISTANCE) return "Delivery Distance";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.DELIVERY_PAYOUT) return "Delivery Payout";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.DROP_PROBABILITY) return "Luck";
        else if (levelUpgrade.upgradeType == LevelUpgradeType.XP_GAIN) return "Experience Gain";
        else return "Not implemented yet";
    }

    private string GetLevelUpgradeDescriptionText(LevelUpgrade levelUpgrade)
    {
        if (levelUpgrade.magnitude > 0) return string.Format("{0} \n\n+{1}%", levelUpgrade.upgradeDescription, levelUpgrade.magnitude);
        return string.Format("{0} \n\n{1}%", levelUpgrade.upgradeDescription, levelUpgrade.magnitude);
    }


    public void ChooseLevelUpgrade()
    {
        applyLevelUpgradeEvent.Invoke(currentLevelUpgrade);
    }
}
