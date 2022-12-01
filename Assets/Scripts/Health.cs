using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class Health : MonoBehaviour
{
    [SerializeField] public GameObjectFloatSOEvent callOnTakeDamageEvent;
    [SerializeField] public GameObjectBoolSOEvent callOnDeathEvent;
    [SerializeField] GameModifiers gameModifiers;
    [SerializeField] PersistentModifiers persistantModifiers;

    [SerializeField] public float maxHp;
    [SerializeField] float currentHp;
    [SerializeField] public float hpRegenRate;

    [SerializeField] public float shieldMaxHp;
    [SerializeField] float shieldCurrentHp;
    [SerializeField] public float shieldRegenRate;
    [SerializeField] public float shieldOnHitRegenCooldown;
    [SerializeField] float currentShieldRegenCooldown;

    public float GetCurrentHp() => currentHp;
    public float GetCurrentShieldHp() => shieldCurrentHp;
    public float ManualSetCurrentHp(float value) => currentHp = Mathf.Clamp(value, 0, maxHp);
    public float ManualSetCurrentShieldHp(float value) => shieldCurrentHp = Mathf.Clamp(value, 0, shieldMaxHp);

    // When taking damage, first damages the shield, and puts the shield's regeneration on cooldown. The, subtracts any remaining damage from currentHp.
    public void TakeDamage(float dmgValue)
    {
        float usedDamage = dmgValue;
        if (gameModifiers) usedDamage = (usedDamage + gameModifiers.GetAdditionValueOfType(UpgradeType.DAMAGE_TAKEN_CURRENT_GAME))
         * gameModifiers.GetMultiplicationValueOfType(UpgradeType.DAMAGE_TAKEN_CURRENT_GAME);
        if (persistantModifiers) usedDamage *= persistantModifiers.damageTakenMod;

        shieldCurrentHp -= usedDamage;
        currentShieldRegenCooldown = shieldOnHitRegenCooldown;

        if (callOnTakeDamageEvent) callOnTakeDamageEvent.Invoke(gameObject, dmgValue);

        if (shieldCurrentHp < 0)
        {
            float hpDmgValue = - shieldCurrentHp;
            shieldCurrentHp = 0;
            currentHp -= hpDmgValue;
            CheckAlive();
        }

    }

    private void FixedUpdate() {
        currentShieldRegenCooldown -= Time.fixedDeltaTime;
        if (currentShieldRegenCooldown <= 0) ManualSetCurrentShieldHp(shieldCurrentHp + (shieldRegenRate * Time.fixedDeltaTime));

        ManualSetCurrentHp(currentHp + (hpRegenRate * Time.fixedDeltaTime));
    }

    // Calls event when currentHp <= 0. Sends its parent's GameObject in the Event Invocation.
    public void CheckAlive()
    {   
        if (currentHp <= 0) 
        {
            if (currentHp > -10000) callOnDeathEvent.Invoke(transform.parent.gameObject, true);
            else callOnDeathEvent.Invoke(transform.parent.gameObject, false);
        }
    }
}
