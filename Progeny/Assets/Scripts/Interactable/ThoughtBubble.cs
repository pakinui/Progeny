using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtBubble : MonoBehaviour
{
    public float timeRemaining = 3;
    private bool showBubble = false;
    //player
    private Player player;
    private GameObject speechBubble;
    private Rigidbody2D rb;
    public GameObject bubble;
    public TextMeshProUGUI theText;
     

    
    


    // Start is called before the first frame update
    void Start()
    {
        bubble.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        //speech bubbles rb
        rb = bubble.GetComponent<Rigidbody2D>();
        //rb.SetActive(true);


    }


    public void SetBubbleText(string text){
        theText.text = text;
    }


    public void ShowBubble(){
        bubble.SetActive(true);
        showBubble = true;
    }

    public void hideBubble(){
        bubble.SetActive(false);
        showBubble = false;
    }

    // Update is called once per frame
    void Update()
    {

        
        if(timeRemaining > 0 || showBubble){
            timeRemaining -= Time.deltaTime;

            rb.transform.position = new Vector3 (player.transform.position.x + 0.7f, player.transform.position.y, player.transform.position.z);
        }else{   
              
            //speechBubble.SetActive(false);
   
        }

    }

    
}
