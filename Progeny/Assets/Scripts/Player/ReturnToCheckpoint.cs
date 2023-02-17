using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToCheckpoint : MonoBehaviour
{
    
    
    private GameMaster gm;
    private Player player;
    public GameObject deathPanel;

    public bool reset = false;

    private float timeReminaing = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeReminaing < 0 && reset){
            reset = false;
            timeReminaing = 1;
        }else if(reset){
           timeReminaing -= Time.deltaTime;
        }
    }

    public void resetLevel(){
        Debug.Log("reset");
        player.resetPlayer();
        reset = true;
        //deathPanel.SetActive(false);
    }


    
}
