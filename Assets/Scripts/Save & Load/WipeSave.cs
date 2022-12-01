using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WipeSave : MonoBehaviour
{
    [SerializeField] LoadOnStart loadOnStartLogic;
    [SerializeField] TextMeshProUGUI wipeButtonText;
    [SerializeField] PlayerLevel playerLevelLogic;
    public int pressesUntilWipe = 5;
    public void OnPressed()
    {
        pressesUntilWipe --;
        wipeButtonText.SetText(string.Format("Press {0} Times to Wipe Save Data", pressesUntilWipe));
        if (pressesUntilWipe == 0) WipeData();
    }

    public void WipeData()
    {
        Debug.Log("Wiping Save Data");
        PlayerPrefs.DeleteAll();
        playerLevelLogic.currentLevel = 1;
        loadOnStartLogic.RunSetupSequence();
    }
}
