using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneTransitionLogic : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject shipSelectionPanel;
    [SerializeField] GameObject perksPanel;
    [SerializeField] GameObject howToPlayPanel;

    [SerializeField] GameObject howToPlayObjects;

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void ToShipSelectionPanel()
    {
        startPanel.SetActive(false);
        shipSelectionPanel.SetActive(true);
        perksPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        howToPlayObjects.SetActive(false);
    }

    public void ToStartPanel()
    {
        startPanel.SetActive(true);
        shipSelectionPanel.SetActive(false);
        perksPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        howToPlayObjects.SetActive(false);
    }

    public void ToPerksPanel()
    {
        startPanel.SetActive(false);
        shipSelectionPanel.SetActive(false);
        perksPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        howToPlayObjects.SetActive(false);
    }

    public void ToHowToPlayPanelAndObjects()
    {
        startPanel.SetActive(false);
        shipSelectionPanel.SetActive(false);
        perksPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        howToPlayObjects.SetActive(true);
    }
}
