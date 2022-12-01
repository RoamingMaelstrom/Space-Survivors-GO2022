using UnityEngine;
using TMPro;
using SOEvents;
using System;

public class ObjectiveDisplayLogic : MonoBehaviour
{
    [SerializeField] GameObjectSOEvent objectiveCreatedEvent;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI payoutText;
    [SerializeField] TextMeshProUGUI distanceText;

    [SerializeField] Rigidbody2D playerBody;
    GameObject currentObjective;

    private void Awake() {
        objectiveCreatedEvent.AddListener(LoadNewObjectiveData);
    }

    private void LoadNewObjectiveData(GameObject objective)
    {
        currentObjective = objective;

        ObjectiveMain objectiveMainScript = objective.GetComponent<ObjectiveMain>();
        descriptionText.SetText(objectiveMainScript.objectiveDescription);
        payoutText.SetText(string.Format("Payout: ${0:0.00}", objectiveMainScript.playerReward));
        distanceText.SetText(GetDistanceText(playerBody, objective));
    }

    private string GetDistanceText(Rigidbody2D playerBody, GameObject objective)
    {
        float distance = (playerBody.transform.position - objective.transform.position).magnitude;

        return string.Format("Distance: {0:0.0}mi", distance * 10);
    }

    private void FixedUpdate() 
    {
        if (currentObjective) distanceText.SetText(GetDistanceText(playerBody, currentObjective));
    }
}
