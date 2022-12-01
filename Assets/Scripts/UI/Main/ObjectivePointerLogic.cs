using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;
using System;

public class ObjectivePointerLogic : MonoBehaviour
{
    [SerializeField] GameObjectSOEvent objectiveCreatedEvent;
    [SerializeField] GameObject pointerPrefab;
    [SerializeField] GameObject currentObjective;
    [SerializeField] GameObject player;

    GameObject pointer;

    private void Awake() 
    {
        objectiveCreatedEvent.AddListener(GetNewObjective);

    }

    private void Start() {
        pointer = Instantiate(pointerPrefab, transform);   
    }

    private void GetNewObjective(GameObject objective)
    {
        currentObjective = objective;
    }

    private void Update() 
    {
        if (!currentObjective) return;
        Vector3 dirVector = (currentObjective.transform.position - player.transform.position);
        dirVector.z = player.transform.position.z;
        dirVector = dirVector.normalized;

        pointer.transform.position = player.transform.position + (dirVector * 5);

        float newRotation = - Mathf.Atan2(dirVector.x, dirVector.y) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.Euler(0, 0, newRotation);
    }

}
