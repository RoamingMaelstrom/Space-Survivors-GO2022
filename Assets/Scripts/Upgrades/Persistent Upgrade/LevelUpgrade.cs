using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpgrade
{
    public LevelUpgradeType upgradeType;
    public string upgradeDescription;
    public float magnitude;
}



public enum LevelUpgradeType
{
    MAX_HP,
    DAMAGE_TAKEN,
    DELIVERY_DISTANCE,
    DELIVERY_PAYOUT, 
    MOVESPEED,
    DAMAGE,
    ATTACK_COOLDOWN,
    DROP_PROBABILITY,
    XP_GAIN

}


