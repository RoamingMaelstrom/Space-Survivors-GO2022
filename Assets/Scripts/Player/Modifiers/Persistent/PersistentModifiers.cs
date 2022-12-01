using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentModifiers : MonoBehaviour
{
    public float maxHpMod = 1; // Tick
    public float damageTakenMod = 1; // Tick
    public float moveSpeedMod = 1; // Tick
    public float damageMod = 1; // Tick
    public float attackSpeedMod = 1; // Tick
    public float dropProbabilityMod = 1; // Tick
    public float xpGainMod = 1; // Tick
    public float deliveryDistanceMod = 1; // Tick
    public float deliveryPayoutMod = 1; // Tick
}



public abstract class OtherPotentialModifiers
{
    public float rotationSpeedMod;
    public float lifeStealMod;
    public float damageDealerSizeMod;
    public float dropPickupRadiusMod;
    public float damageDealerLifespanMod;
    public float hpRegen;
    public float shieldHealthMod;
    public float shieldRegenMod;
    public float shieldRegenCooldownMod;
    public float critChanceMod;
    public float rangeMod;
    public float dodgeChance;
}
