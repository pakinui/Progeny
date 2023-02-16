using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    
    public Sprite[] healthBars; //20 bars overall
    public GameObject bars;
    private SpriteRenderer currentBars;

    private Player player;
    private int maxHealth; //player max health = 300
    private float health = 300;
    private int currIndex = 0;
    private bool dead = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        currentBars = bars.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health != player.GetCurrentHealth() && !dead){
        

        int minus = (int) (health - player.GetCurrentHealth()) /10;
        currIndex = currIndex + minus;
        health = player.GetCurrentHealth();
        //Debug.Log(player.GetCurrentHealth() + ", " + currIndex);
        
        
        }
        

        if(currIndex > 28 && !dead){
            bars.SetActive(false); // to show 0 health
            dead = true;
            //Debug.Log("dead bitch");
            health = 300;
            currIndex = 0;
        
            currentBars.sprite = healthBars[0];
            
            
        }else if (!dead){
            currentBars.sprite = healthBars[currIndex];
        
        }
        
    }

    public void ResetHealthbar(){
        
       
        bars.SetActive(true);
        dead =  false;
        gameObject.SetActive(true);
        //Debug.Log("reset: " + player.GetCurrentHealth() + ", " + currIndex);
    }


    public void HideHealth(){
        gameObject.SetActive(false);
    }

    public void ShowHealth(){
        gameObject.SetActive(true);
    }
}
