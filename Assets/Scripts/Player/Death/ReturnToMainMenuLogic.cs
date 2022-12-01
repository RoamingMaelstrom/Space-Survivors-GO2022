using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenuLogic : MonoBehaviour
{
    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
