using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToCheckpoint : MonoBehaviour
{
    
    
    private GameMaster gm;
    private Player player;
    public GameObject deathPanel;

    public bool reset = false;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetLevel(){

        player.SetCurrentHealth(player.maxHealth);
        player.transform.position = gm.getLastCheckpoint();
        player.startPlayerMovement();
        reset = true;
        //deathPanel.SetActive(false);
    }


    
}
