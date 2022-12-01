using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class ObjectiveMain : MonoBehaviour
{
    [SerializeField] GameObjectSOEvent objectiveCreatedEvent;
    public Collider2DCollider2DSOEvent objectiveReachedEvent;
    [SerializeField] Collider2D ownCollider;
    public string objectiveDescription;
    public float playerReward;


    private void Start() {
        if (!ownCollider) ownCollider = GetComponent<Collider2D>();
        objectiveCreatedEvent.Invoke(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.tag == "Player") objectiveReachedEvent.Invoke(other, ownCollider);
    }
}
