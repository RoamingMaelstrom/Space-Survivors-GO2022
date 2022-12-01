using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintsLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] [TextArea] List<string> hints = new List<string>();
    int hintNum;

    public void GetHint()
    {
        hintNum = PlayerPrefs.GetInt("hintNum");
        if (hintNum > hints.Count - 1) SetRandomHint();
        else SetHintAtIndex(hintNum);
        hintNum ++;
        PlayerPrefs.SetInt("hintNum", hintNum);
        PlayerPrefs.Save();
    }

    private void SetRandomHint()
    {
        hintText.SetText(hints[Random.Range(0, hints.Count)]);
    }

    
    private void SetHintAtIndex(int index)
    {
        hintText.SetText(hints[index]);
    }

}
