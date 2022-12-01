using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class PickupLogic : MonoBehaviour
{
    [SerializeField] DroppedItemMainSOEvent pickupItemEvent;
    [SerializeField] GameObjectBoolSOEvent despawnEvent;
    [SerializeField] FloatSOEvent updatePlayerMoneyEvent;
    [SerializeField] DroppedItemMainSOEvent generateItemEffectEvent;
    [SerializeField] SfxMain sfxMain;
    [SerializeField] AudioClip pickupClip;

    private void OnTriggerEnter2D(Collider2D other) {
        pickupItemEvent.Invoke(other.GetComponent<DroppedItemMain>());
    }

    private void Awake() {
        pickupItemEvent.AddListener(ProcessDroppedItem);
    }

    public void ProcessDroppedItem(DroppedItemMain droppedItem)
    {
        if (droppedItem.droppableType == DroppableType.MONEY) updatePlayerMoneyEvent.Invoke(droppedItem.value1);
        despawnEvent.Invoke(droppedItem.gameObject, false);
        sfxMain.PlaySound(pickupClip, Camera.main.transform.position, 0.1f, true);
    }

}
