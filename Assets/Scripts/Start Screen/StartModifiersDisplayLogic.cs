using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartModifiersDisplayLogic : MonoBehaviour
{
    public List<Transform> statDisplayContainers = new List<Transform>();
    public LoadOnStart loadOnStartScript;

    // Todo: This is the a top contender for the worst code in this project. Redo it.
    private void FixedUpdate() 
    {
        statDisplayContainers[0].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Attack Speed");
        statDisplayContainers[0].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%",Mathf.Abs(100 - (100 / loadOnStartScript.attackSpeedMod))));

        statDisplayContainers[1].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Damage");
        statDisplayContainers[1].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.damageMod - 1) * 100));

        statDisplayContainers[2].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Damage Taken");
        statDisplayContainers[2].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("- {0:0.00}%",Mathf.Abs(100 - (100 / loadOnStartScript.damageTakenMod))));

        statDisplayContainers[3].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Delivery Distance");
        statDisplayContainers[3].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("- {0:0.00}%",Mathf.Abs(100 - (100 / loadOnStartScript.deliveryDistanceMod))));

        statDisplayContainers[4].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Delivery Payout");
        statDisplayContainers[4].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.deliveryPayoutMod - 1) * 100));

        statDisplayContainers[5].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Drop Probability");
        statDisplayContainers[5].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.dropProbabilityMod - 1) * 100));

        statDisplayContainers[6].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Ship Health");
        statDisplayContainers[6].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.maxHpMod - 1) * 100));

        statDisplayContainers[7].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Ship Speed");
        statDisplayContainers[7].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.moveSpeedMod - 1) * 100));

        statDisplayContainers[8].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Experience Gain");
        statDisplayContainers[8].GetChild(2).GetComponent<TextMeshProUGUI>().SetText(string.Format("+ {0:0.00}%", (loadOnStartScript.xpGainMod - 1) * 100));
    
        statDisplayContainers[9].GetChild(1).GetComponent<TextMeshProUGUI>().SetText("");
        statDisplayContainers[9].GetChild(2).GetComponent<TextMeshProUGUI>().SetText("");
    }
}
