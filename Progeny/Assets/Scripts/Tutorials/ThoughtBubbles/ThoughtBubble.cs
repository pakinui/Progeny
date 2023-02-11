using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtBubble : MonoBehaviour
{
    private float timeRemaining = 3;
    public bool showBubble = false;
    
    public GameObject bubble;
    public TextMeshProUGUI theText;

    

    //player
    private Player player;
    private Rigidbody2D rb;
    private bool bubbleOnTimer = false;
     

    // Start is called before the first frame update
    void Start()
    {
        bubble.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();//rb to move thought bubble
    }

    /**
        method to set the text of the thoughtbubble
    */
    public void SetBubbleText(string text){
        theText.text = text;
    }

    /**
        method to show the thoughtbubble
        will be shown until hideBubble() is called
    */
    public void ShowBubble(){
        hideBubble();//incase another bubble is open atm
        bubble.SetActive(true);
        showBubble = true;
    }

    /**
        method to hide the thoughtbubble
    */
    public void hideBubble(){
        bubble.SetActive(false);
        showBubble = false;
    }

    /**
        method to show the thoughtbubble
        will automatically hide the thoughtbubble after
        a set amount of seconds
    */
    public void ShowBubbleForSeconds(float sec){
        bubbleOnTimer = true;
        timeRemaining = sec;
        ShowBubble();
        
        

    }

    // Update is called once per frame
    void Update()
    {

        if(bubbleOnTimer){
            //if bubble is on a timer
            if(timeRemaining > 0 && showBubble){
                timeRemaining -= Time.deltaTime;

                rb.transform.position = new Vector3 (player.transform.position.x - 6.0f, player.transform.position.y + 0.8f, player.transform.position.z);
            }else{
                //timer is up
                hideBubble();
                bubbleOnTimer = false;
            }
        }else{
            //if bubble is not on a timer
            if(showBubble){
                rb.transform.position = new Vector3 (player.transform.position.x - 6.0f, player.transform.position.y + 0.8f, player.transform.position.z);
            }
        }
    }

    
    
}

