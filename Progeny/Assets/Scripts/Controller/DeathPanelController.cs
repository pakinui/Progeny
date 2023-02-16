using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelController : MonoBehaviour
{

    private Player player;
    private ReturnToCheckpoint rtc;
    private Canvas canvas;
    

    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rtc = player.GetComponent<ReturnToCheckpoint>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        Time.timeScale = 0;
        player.stopPlayerMovement();
    }

    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //reset from checkpoint
        //Time.timeScale = 1;
        
        rtc.resetLevel();
        
        gameObject.SetActive(false);
        
        
    }

    public void ClosePanel()
    {
        
        
    }

    private void OnDisable()
    {
        player.startPlayerMovement();
        Time.timeScale = 1;
        
    }
}
