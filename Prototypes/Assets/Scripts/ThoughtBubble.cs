using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ThoughtBubble : MonoBehaviour
{


    public float timeRemaining = 3;

    //player
    public PlayerMovement player;
    private Rigidbody2D speechBubble;
    public GameObject bubble;

    public GameObject speech;
    


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        //speech bubbles rb
        speechBubble = GetComponent<Rigidbody2D>();
        //speechBubble.SetActive(true);
        

        // if(textFile != null){
        //     textLines = (textFile.text.Split('\n'));
        // }

        
        // if(endLine == 0){
        //     endLine = textLines.Length-1;
        // }

        // nextLine();

    }

    // Update is called once per frame
    void Update()
    {
        //line to display
        //Debug.Log("1: " + textLines[currLine]);
        //theText.text = textLines[currLine];
        //Debug.Log("2: " + theText.text);
       // Vector2 velocity = new Vector2(player.speed, player.rb.velocity.y);
        
        if(timeRemaining > 0){
            timeRemaining -= Time.deltaTime;
            //speechBubble.velocity = player.rb.velocity;
            //speechBubble.transform.position = player.transform.position;
            speechBubble.transform.position = new Vector3 (player.transform.position.x + 0.7f, player.transform.position.y, player.transform.position.z);
        }else{

           
              
                speech.SetActive(false);
          
                //Debug.Log("time up");
                //fade thought bubble
            
            
        }



        // if(Input.GetKeyDown(KeyCode.Space)){
        //     currLine++;
        //     nextLine();
        // }

        



    }

    // private void nextLine(){


    //     if(currLine > endLine){
    //         textBox.SetActive(false);
    //     }else{
    //         theText.text = textLines[currLine];
    //     }
        

    // }
}
