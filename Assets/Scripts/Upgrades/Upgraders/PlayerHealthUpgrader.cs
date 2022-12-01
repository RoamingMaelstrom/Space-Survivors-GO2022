using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUpgrader : Upgrader
{
    [SerializeField] Health playerHealthScript;

    public override void ApplyUpgrade(Upgrade upgrade, int weaponTypeID)
    {
        if (upgrade.upgradeType == UpgradeType.MAX_HP_CURRENT_GAME) 
        {
            if (upgrade.isPercent) playerHealthScript.maxHp *= 1 + (upgrade.magnitude / 100f);
            else playerHealthScript.maxHp += upgrade.magnitude;
            playerHealthScript.ManualSetCurrentHp(playerHealthScript.maxHp);
        }
        else if (upgrade.upgradeType == UpgradeType.HEALTH_REGEN_CURRENT_GAME) 
        {
            if (upgrade.isPercent) playerHealthScript.hpRegenRate *= 1 + (upgrade.magnitude / 100f);
            else playerHealthScript.hpRegenRate += upgrade.magnitude;
        }
        else if (upgrade.upgradeType == UpgradeType.SHIELD_MAX_HP)
        {
            if (upgrade.isPercent) playerHealthScript.shieldMaxHp *= 1 + (upgrade.magnitude / 100f);
            else playerHealthScript.shieldMaxHp += upgrade.magnitude;
        }
        else if (upgrade.upgradeType == UpgradeType.SHIELD_REGEN_RATE)
        {
            if (upgrade.isPercent) playerHealthScript.shieldRegenRate *= 1 + (upgrade.magnitude / 100f);
            else playerHealthScript.shieldRegenRate += upgrade.magnitude;
        }
        else if (upgrade.upgradeType == UpgradeType.SHIELD_REGEN_COOLDOWN)
        {
            if (upgrade.isPercent) playerHealthScript.shieldOnHitRegenCooldown *= 1 + (upgrade.magnitude / 100f);
            else playerHealthScript.shieldOnHitRegenCooldown += upgrade.magnitude;
        }
    }

    public override void ApplyLevelUpgrade(LevelUpgrade levelUpgrade)
    {
        if (levelUpgrade.upgradeType == LevelUpgradeType.MAX_HP) playerHealthScript.maxHp *= 1f + (levelUpgrade.magnitude / 100f);
    }
}
