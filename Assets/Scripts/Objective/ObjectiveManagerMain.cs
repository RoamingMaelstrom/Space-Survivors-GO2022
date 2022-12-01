using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOEvents;

public class ObjectiveManagerMain : MonoBehaviour
{
    [SerializeField] Collider2DSOEvent createObjectiveEvent;
    [SerializeField] Collider2DCollider2DSOEvent objectiveReachedEvent;
    [SerializeField] GameModifiers gameModifiers;
    [SerializeField] PersistentModifiers persistantModifiers;
    [SerializeField] GameObject objectivePrefab;
    [SerializeField] List<GameObject> npcShipPrefabs;
    [SerializeField] float baseDistanceFromPlayer;
    [SerializeField] float baseMoneyReward;
    [SerializeField] [Range(0f, 1f)] float distanceRandomness;
    [SerializeField] [Range(0f, 50f)] float distanceIncreaseValue = 10f;
    [SerializeField] float moneyPerUnitDistance = 1f;

    [SerializeField] Collider2D playerCollider;
    [SerializeField] PlayerMoney playerMoneyScript;

    [SerializeField] List<string> baseDescriptions = new List<string>();
    [SerializeField] List<string> firstNames = new List<string>();
    [SerializeField] List<string> lastNames = new List<string>();


    private void Start() {
        createObjectiveEvent.AddListener(CreateNewObjective);
        objectiveReachedEvent.AddListener(GivePlayerReward);
        objectiveReachedEvent.AddListener(DestroyObjective);

        createObjectiveEvent.Invoke(playerCollider);

    }

    // Creates a new target at a random distance from the player, and makes it a child of this object. Does dependency injection too.
    public void CreateNewObjective(Collider2D player)
    {
        float distanceFromPlayer = ((baseDistanceFromPlayer * Random.Range(1 - distanceRandomness, 1 + distanceRandomness)) + 
        gameModifiers.GetAdditionValueOfType(UpgradeType.DELIVERY_DISTANCE_CURRENT_GAME)) 
        * persistantModifiers.deliveryDistanceMod * gameModifiers.GetMultiplicationValueOfType(UpgradeType.DELIVERY_DISTANCE_CURRENT_GAME);

        Vector2 objectiveDir = Random.insideUnitCircle.normalized;
        Vector3 objectivePos = player.transform.position + (new Vector3(objectiveDir.x, objectiveDir.y, 0) * distanceFromPlayer);
        GameObject newObjective = Instantiate(objectivePrefab, objectivePos, Quaternion.identity, transform);

        ObjectiveMain objectiveMain = newObjective.GetComponent<ObjectiveMain>();

        objectiveMain.objectiveReachedEvent = objectiveReachedEvent;
        objectiveMain.playerReward = ((distanceFromPlayer * moneyPerUnitDistance) +
         gameModifiers.GetAdditionValueOfType(UpgradeType.DELIVERY_PAYOUT_CURRENT_GAME)) * persistantModifiers.deliveryPayoutMod *
         gameModifiers.GetMultiplicationValueOfType(UpgradeType.DELIVERY_PAYOUT_CURRENT_GAME);
        objectiveMain.objectiveDescription = GenerateRandomDeliveryDescription();


        GenerateRandomNpcShip(newObjective.transform); 

        baseDistanceFromPlayer += distanceIncreaseValue;
    }

    private string GenerateRandomDeliveryDescription()
    {
        return string.Format("Deliver {0} {1} to {2} {3}", Random.Range(4, 50), baseDescriptions[Random.Range(0, baseDescriptions.Count)],
        firstNames[Random.Range(0, firstNames.Count)], lastNames[Random.Range(0, lastNames.Count)]);
    }

    private void GenerateRandomNpcShip(Transform objectiveTransform)
    {
        GameObject npcShip = Instantiate(npcShipPrefabs[Random.Range(0, npcShipPrefabs.Count)],
         objectiveTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.Euler(0, 0, Random.Range(0, 7) * 45), objectiveTransform);
        npcShip.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f) * Random.Range(0.7f, 0.9f);
    }

    public void GivePlayerReward(Collider2D player, Collider2D objective)
    {
        playerMoneyScript.UpdateMoneyValue(objective.GetComponent<ObjectiveMain>().playerReward);
    }

    // Wrapper method that ensures compatability with objectReachedEvent.
    public void DestroyObjective(Collider2D player, Collider2D objective)
    {
        DestroyObjective(objective.gameObject);
    }

    // Destroys the current target. Assumes that the objectives parent does not need to be destroyed.
    public void DestroyObjective(GameObject objective)
    {
        Destroy(objective);
    }


}