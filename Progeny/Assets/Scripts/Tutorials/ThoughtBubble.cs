using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThoughtBubble : MonoBehaviour
{
    public float timeRemaining = 3;
    public bool showBubble = false;
    
    public GameObject bubble;
    public TextMeshProUGUI theText;

    //player
    private Player player;
    private Rigidbody2D rb;
     

    // Start is called before the first frame update
    void Start()
    {
        bubble.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        
        rb = gameObject.GetComponent<Rigidbody2D>();
       


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

            rb.transform.position = new Vector3 (player.transform.position.x - 6.0f, player.transform.position.y + 0.8f, player.transform.position.z);
        }else{   
              
            //speechBubble.SetActive(false);
   
        }

    }

    
}
