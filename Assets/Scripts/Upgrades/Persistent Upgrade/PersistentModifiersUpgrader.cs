using UnityEngine;
using SOEvents;

public class PersistentModifiersUpgrader : MonoBehaviour
{
    [SerializeField] LevelUpgradeSOEvent applyLevelUpgradeEvent;
    [SerializeField] PersistentModifiers persistentModifiers;
    [SerializeField] SaveAfterLevelling saveAfterLevellingScript;
    [SerializeField] PlayerLevel playerLevelScript;

    private void Awake() {
        applyLevelUpgradeEvent.AddListener(UpdatePersistentModifiers);
    }

    private void UpdatePersistentModifiers(LevelUpgrade levelUpgrade)
    {
        LevelUpgradeType upType = levelUpgrade.upgradeType;
        float mag = levelUpgrade.magnitude;
        if (upType == LevelUpgradeType.ATTACK_COOLDOWN) persistentModifiers.attackSpeedMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.DAMAGE) persistentModifiers.damageMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.DAMAGE_TAKEN) persistentModifiers.damageTakenMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.DELIVERY_DISTANCE) persistentModifiers.deliveryDistanceMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.DELIVERY_PAYOUT) persistentModifiers.deliveryPayoutMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.MAX_HP) persistentModifiers.maxHpMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.DROP_PROBABILITY) persistentModifiers.dropProbabilityMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.XP_GAIN) persistentModifiers.xpGainMod *= (1f + (mag / 100f));
        else if (upType == LevelUpgradeType.MOVESPEED) persistentModifiers.moveSpeedMod *= (1f + (mag / 100f));

        Debug.Log("Saving Persistent Data");

        saveAfterLevellingScript.SavePersistentModData(persistentModifiers);
        saveAfterLevellingScript.SavePlayerLevelData(playerLevelScript);
    }
}
