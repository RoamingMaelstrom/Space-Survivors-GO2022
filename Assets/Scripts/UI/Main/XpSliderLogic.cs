using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpSliderLogic : MonoBehaviour
{
    [SerializeField] Slider xpSlider;
    [SerializeField] PlayerLevel playerLevelScript;

    private void FixedUpdate() {
        xpSlider.value = playerLevelScript.xpProgressInCurrentLevel / playerLevelScript.levelBounds[playerLevelScript.currentLevel - 1];
    }
}
