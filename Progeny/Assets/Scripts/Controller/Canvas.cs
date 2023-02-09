using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    private Player player;
    private GameObject deathPanel;
    private GameObject pausePanel;

    void Start()
    {
        // assigning references
        player = GameObject.Find("Player").GetComponent<Player>();
        deathPanel = GameObject.Find("DeathPanel");
        deathPanel.SetActive(false);
        pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentHealth() <= 0)
        {
            deathPanel.SetActive(true);
        }
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !pausePanel.activeSelf)
        {
            Debug.Log("Pause Game");
            pausePanel.SetActive(true);
        }
    }
}
