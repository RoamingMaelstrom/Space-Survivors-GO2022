using UnityEngine;
using SOEvents;

public abstract class Upgrader : MonoBehaviour
{
    [SerializeField] UpgradeInfoIntSOEvent applyUpgradeEvent;
    [SerializeField] LevelUpgradeSOEvent applyLevelUpgradeEvent;

    private void Awake() 
    {
        applyUpgradeEvent.AddListener(ApplyUpgrades);
        applyLevelUpgradeEvent.AddListener(ApplyLevelUpgrade);
    }

    void ApplyUpgrades(UpgradeInfo upgradeInfo, int weaponTypeID)
    {
        foreach (var upgrade in upgradeInfo.upgrades)
        {
            ApplyUpgrade(upgrade, weaponTypeID);
        }
    }

    public abstract void ApplyUpgrade(Upgrade upgrade, int weaponTypeID);

    public abstract void ApplyLevelUpgrade(LevelUpgrade levelUpgrade);

}
