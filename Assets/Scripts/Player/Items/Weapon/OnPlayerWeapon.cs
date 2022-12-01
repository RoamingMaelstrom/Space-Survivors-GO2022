using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerWeapon : Weapon
{

    private void Awake() {
        StartCoroutine(KeepDamageDealerOnPlayer());
    }
    public override void Fire()
    {
        if (currentDamageDealerObject) currentDamageDealerObject.GetComponent<LifeSpanLimiter>().spanRemaining = 0;
        requestDamageDealerEvent.Invoke(this);
        currentDamageDealerObject.transform.position = transform.position;
    }

    IEnumerator KeepDamageDealerOnPlayer()
    {
        while (gameObject.activeInHierarchy)
        {

            if (currentDamageDealerObject) currentDamageDealerObject.transform.position = transform.position;
            yield return new WaitForFixedUpdate();
        }
        yield return null;
    }
}
