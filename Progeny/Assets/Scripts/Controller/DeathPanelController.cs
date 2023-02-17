using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelController : MonoBehaviour
{

    private Player player;
    private ReturnToCheckpoint rtc;
    private Canvas canvas;
    public GameObject healthBar;

    private HealthBar hb;

    private bool restarted = false;
    

    private void OnEnable()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        rtc = player.GetComponent<ReturnToCheckpoint>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        hb = healthBar.GetComponent<HealthBar>();
        Time.timeScale = 0;
        player.stopPlayerMovement();
        healthBar.SetActive(false);
        restarted = false;
    }

    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {

        
     

        player = GameObject.Find("Player").GetComponent<Player>();
        rtc = player.GetComponent<ReturnToCheckpoint>();
        rtc.resetLevel();
        
        gameObject.SetActive(false);
       
        
        //healthBar.SetActive(true);
        
        
        //reset from checkpoint
        //Time.timeScale = 1;
        
        
        
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
