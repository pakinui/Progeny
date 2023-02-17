using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    
    //public iPad ipad; // get know when they make their final decision

   
    public GameObject fireText;
    public GameObject noFireText;
    public GameObject fireAnim;

    public bool showing = false;
    public int decision = 0;// 0 = fire, 1 = no fire

    private float fireTimer = 2f;
    private Player player;

    private bool fire = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(showing){
            // they made their decision
           
            
            
            if(decision == 0){
                Debug.Log("asd game end");
                fireText.SetActive(true);
                //fire
                player.stopPlayerMovement();
                
            
                
            }else{
Debug.Log("asdasdf game end");
                //no fire
                player.finalDecisionMade = true;
                
                
            }
        }else{
            Debug.Log("aaaaasd game end");
        }
    }

    public void EndDeathScreen(){
        Time.timeScale = 0;
        
        gameObject.SetActive(true);
        noFireText.SetActive(true);
    }

    public void RedirectToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
