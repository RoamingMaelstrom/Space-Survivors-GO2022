using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class DamageDealerConfigurer : MonoBehaviour
{
    [SerializeField] public GameObjectBoolSOEvent despawnEvent;
    [SerializeField] public WeaponSOEvent requestDamageDealerEvent;
    [SerializeField] public ObjectPoolMain objectPool;
    [SerializeField] public GameModifiers gameModifiers;
    [SerializeField] public PersistentModifiers persistentModifiers;
    [SerializeField] public SfxMain sfxMain;

    public List<DamageDealerProfile> damageDealerProfiles = new List<DamageDealerProfile>();

    private void Awake() 
    {
        requestDamageDealerEvent.AddListener(ProvideNewDamageDealerToWeapon);
    }

    public void ProvideNewDamageDealerToWeapon(Weapon weapon)
    {
        weapon.currentDamageDealerObject = CreateDamageDealer(weapon.damageDealerTypeID, weapon);
    }

    public GameObject CreateDamageDealer(int damageDealerTypeID, Weapon weapon)
    {
        GameObject damageDealerObject = objectPool.GetObject("Damage Dealers");

        DamageDealer damageDealer = damageDealerObject.GetComponent<DamageDealer>();
        DamageDealerProfile profile = GetDamageDealerProfile(damageDealerTypeID);

        damageDealer.weaponCreatedBy = weapon;

        damageDealer.despawnEvent = despawnEvent;
        damageDealer.damageValue = GetGameDependentModifiedValue(profile.damageValue, weapon.damageAdd, weapon.damageMod,
         UpgradeType.DAMAGE_CURRENT_GAME) * persistentModifiers.damageMod;
        damageDealer.dotDamageValue = GetGameDependentModifiedValue(profile.dotDamageValue, weapon.dotDamageAdd, weapon.dotDamageMod,
         UpgradeType.DAMAGE_DOT_CURRENT_GAME) * persistentModifiers.damageMod;
        damageDealer.dotInterval = GetGameDependentModifiedValue(profile.dotInterval, weapon.dotIntervalAdd, weapon.dotIntervalMod,
         UpgradeType.DOT_INTERVAL_CURRENT_GAME);
        damageDealer.life = profile.basePiercing + weapon.piercingAdd;
        damageDealer.ManuallyRestartDotDamageCoroutine();

        damageDealer.deathType = profile.deathType;
        damageDealer.onDeathDamageDealerID = profile.onDeathDamageDealerID;
        damageDealer.deathCustomValue = profile.deathCustomValue;


        damageDealerObject.GetComponent<SpriteRenderer>().sprite = profile.sprite;
        damageDealerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(profile.baseMunitionSpeed, 0);
        damageDealerObject.transform.localScale = profile.scale;
        damageDealerObject.GetComponent<CapsuleCollider2D>().size = profile.triggerScale;



        damageDealerObject.GetComponent<LifeSpanLimiter>().onLifeEndEvent = despawnEvent;
        damageDealerObject.GetComponent<LifeSpanLimiter>().StartCountdown(profile.lifeSpan * Random.Range(0.95f, 1.05f));

        DamageDealerBehaviourLogic behaviourLogic =  damageDealerObject.GetComponent<DamageDealerBehaviourLogic>();
        behaviourLogic.behaviour = profile.behaviourType;
        behaviourLogic.customValue = profile.customValue;
        behaviourLogic.customValue2 = profile.customValue2;

        // Todo: Plays sound on DamageDealerCreation. This really should be an event call that is then handled by a separate script.
        if (profile.creationSoundClip) sfxMain.PlaySound(profile.creationSoundClip, weapon.transform.position,
         profile.creationSoundVolume, profile.creationSoundIsDiscrete);

        return damageDealerObject;
    }

    private DamageDealerProfile GetDamageDealerProfile(int damageDealerTypeID)
    {
        foreach (var profile in damageDealerProfiles)
        {
            if (profile.damageDealerTypeID == damageDealerTypeID) return profile;
        }

        throw new System.Exception(string.Format("Could not find profile with ID {0}", damageDealerTypeID));
    }


    public float GetGameDependentModifiedValue(float profileValue, float weaponAdd, float weaponMod, UpgradeType upgradeType)
    {
        return (profileValue + gameModifiers.GetAdditionValueOfType(upgradeType) + weaponAdd) 
        * weaponMod * gameModifiers.GetMultiplicationValueOfType(upgradeType);
    }
}
