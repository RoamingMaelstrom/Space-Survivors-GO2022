using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class TestingWeaponAttaching : MonoBehaviour
{
    [SerializeField] IntSOEvent addItemToPlayerEvent;
    [SerializeField] Transform playerParent;

    public void AttachWeaponToPlayer()
    {
        addItemToPlayerEvent.Invoke(500);
    }

    public void AttachLaserBeamToPlayer()
    {
        addItemToPlayerEvent.Invoke(501);
    }

    public void AttachSingleShotToPlayer()
    {
        addItemToPlayerEvent.Invoke(502);
    }
}
