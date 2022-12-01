using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public GameObjectBoolSOEvent despawnEvent;
    [SerializeField] public float damageValue;
    [SerializeField] public float dotDamageValue;
    [SerializeField] float DotInterval; 
    [SerializeField] public float life;

    public bool alive = true;
    bool dotCoroutineRunning = false;

    public DDDeathType deathType;
    public int onDeathDamageDealerID;
    public float deathCustomValue;

    List<ObjectDotInfo> collidingGameObjects = new List<ObjectDotInfo>();

    public Weapon weaponCreatedBy;

    // Ensures that dotInterval cannot be shorter than physics update time (0.02f).
    public float dotInterval 
    {
        get => DotInterval;
        set => DotInterval = Mathf.Max(Time.fixedDeltaTime, value);
    }

    // DamageDealer will only check for dot 
    private void Start() {
        if (dotDamageValue > 0) StartCoroutine(DotDamageCoroutine());
    }

    // Stops DotDamageCoroutine if it is running, and then starts a new DotDamageCoroutine. Also sets alive to true (otherwise coroutine would immediately stop).
    public void ManuallyRestartDotDamageCoroutine()
    {
        if (dotCoroutineRunning) StopCoroutine(DotDamageCoroutine());
        collidingGameObjects.Clear();
        alive = true;
        if (dotDamageValue <= 0) return;
        StartCoroutine(DotDamageCoroutine());
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out Health otherHealth)) 
        {
            if (damageValue > 0) 
            {
                DealDamage(damageValue, otherHealth);
            }
            collidingGameObjects.Add(new ObjectDotInfo(otherHealth));
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        int targetIndex = -1;
        for (int i = 0; i < collidingGameObjects.Count; i++)
        {
            if (collidingGameObjects[i].objectHealth is null) 
            {
                targetIndex = i;
                break;
            }
            else if (collidingGameObjects[i].objectHealth.gameObject.GetInstanceID() == other.gameObject.GetInstanceID()) 
            {
                targetIndex = i;
                break;
            }
        }
        if (targetIndex == -1) return;
        collidingGameObjects.RemoveAt(targetIndex);
    }



    public void DealDamage(float damageValue, Health healthDamaging)
    {
        if (!alive) return;
        healthDamaging.TakeDamage(damageValue);
        life --;
        if (life <= 0) 
        {
            alive = false;
            collidingGameObjects.Clear();
            // NOTE: This would break if an enemy ran out of damage dealer life. However, this should not happen as enemy damage dealer life should 
            // be practically infinite. May want to fix this bug anyway (has to do with where ObjectIdentfier is/parent structure).
            despawnEvent.Invoke(gameObject, false);
        }
    }


    IEnumerator DotDamageCoroutine()
    {
        dotCoroutineRunning = true;
        while (alive)
        {
            for (int i = collidingGameObjects.Count - 1; i >=0; i--)
            {
                ObjectDotInfo dotItem = collidingGameObjects[i];
                dotItem.timeSinceDamaged -= Time.fixedDeltaTime;
                if (dotItem.timeSinceDamaged <= 0) 
                {
                    DealDamage(dotDamageValue, dotItem.objectHealth);
                    dotItem.timeSinceDamaged += dotInterval;
                }

            }
            yield return new WaitForFixedUpdate();
        }

        dotCoroutineRunning = false;
        yield return null;
    }
}


// Maintains the time since an object (with Health Component) took damage from a particular DamageDealer.
// Needed for dot not dealing damage every physics update and for piercing projectiles.
public class ObjectDotInfo
{
    public Health objectHealth;
    public float timeSinceDamaged;



    public ObjectDotInfo(Health _objectHealth, float _timeSinceDamaged)
    {
        objectHealth = _objectHealth;
        timeSinceDamaged = _timeSinceDamaged;
    }

    public ObjectDotInfo(Health _objectHealth)
    {
        objectHealth = _objectHealth;
        timeSinceDamaged = 0f;
    }
}
