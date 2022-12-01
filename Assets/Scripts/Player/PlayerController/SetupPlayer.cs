using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using System;

public class SetupPlayer : MonoBehaviour
{
    [SerializeField] IntSOEvent addItemToPlayerEvent;
    [SerializeField] UpgradeInfoIntSOEvent applyUpgradeEvent;
    [SerializeField] PlayerShipIdSelected shipIdSelected;
    [SerializeField] List<PlayerShipProfile> playerShipProfiles = new List<PlayerShipProfile>();
    [SerializeField] GameObject playerParentObject;
    [SerializeField] GameObject playerMainBodyObject;
    [SerializeField] PersistentModifiers persistentModifiers;
    [SerializeField] GameObject gameModifiersObject;


    void Start() 
    {
        StartCoroutine(RunSetup());
    }

    public IEnumerator RunSetup()
    {
        yield return new WaitForFixedUpdate();

        PlayerShipProfile profile = playerShipProfiles.Find(x => x.shipID == shipIdSelected.shipTypeID);

        BasePlayerController playerController = playerParentObject.GetComponent<BasePlayerController>();
        playerController.playerThrust = profile.shipThrust * persistentModifiers.moveSpeedMod;
        playerController.playerTorque = profile.shipTorque;
        playerController.maxAngularVelocity = profile.shipMaxRotationSpeed;

        SpriteRenderer playerSpriteRenderer = playerMainBodyObject.GetComponent<SpriteRenderer>();
        playerSpriteRenderer.sprite = profile.shipSprite;
        if (profile.shipID == 9004) playerSpriteRenderer.flipY = true;

        Health healthScript = playerMainBodyObject.GetComponent<Health>();
        healthScript.maxHp = profile.shipBaseMaxHealth * persistentModifiers.maxHpMod;
        healthScript.ManualSetCurrentHp(profile.shipBaseMaxHealth * persistentModifiers.maxHpMod);
        healthScript.hpRegenRate = profile.shipBaseHealthRegen;

        healthScript.shieldMaxHp = profile.shipBaseMaxShieldHealth;
        healthScript.ManualSetCurrentShieldHp(profile.shipBaseMaxHealth);
        healthScript.shieldRegenRate = profile.shipBaseShieldRegen;
        healthScript.shieldOnHitRegenCooldown = profile.shipBaseShieldRegenCooldown;

        gameModifiersObject.GetComponent<PlayerMoney>().money = profile.playerStartingMoney;

        ItemNumberLimit itemNumberLimitScript = gameModifiersObject.GetComponent<ItemNumberLimit>();
        itemNumberLimitScript.maxWeapons = profile.shipMaxNumWeapons;
        itemNumberLimitScript.maxUtilities = profile.shipMaxNumUtilities; 

        foreach (var startingItemId in profile.shipStartingItemIds)
        {
            addItemToPlayerEvent.Invoke(startingItemId);
        }

        applyUpgradeEvent.Invoke(profile.shipStartStatUpgrades, -1);

        yield return null;

    }
}
