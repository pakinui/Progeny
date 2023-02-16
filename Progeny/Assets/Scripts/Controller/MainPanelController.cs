using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanelController : MonoBehaviour
{
    public GameObject optionPanel;
    public GameObject creditsPanel;
    public GameObject continueButton;
    
    private GameMaster gm;
    

    private void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        if (gm.currentLevel != string.Empty)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
    }

    public void ContinueGame()
    {
        Debug.Log("Continue Game");
        gm.ContinueGame();
    }
    
    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene("Level1Art");
    }
    
    public void OptionGame()
    {
        Debug.Log("Option Game");
        optionPanel.SetActive(true);
    }

    public void CreditsGame()
    {
        Debug.Log("Credits Game");
        creditsPanel.SetActive(true);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
