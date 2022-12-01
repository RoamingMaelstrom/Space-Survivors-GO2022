using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarLogic : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Health healthTracking;

    public bool shieldMode = false;

    private void FixedUpdate() {
        if (shieldMode) 
        {
            if (healthTracking.shieldMaxHp == 0) healthBar.gameObject.SetActive(false);
            else
            {
                healthBar.gameObject.SetActive(true);
                healthBar.value = healthTracking.GetCurrentShieldHp() / healthTracking.shieldMaxHp;
            }
        }
        else healthBar.value = healthTracking.GetCurrentHp() / healthTracking.maxHp;
    }
}
