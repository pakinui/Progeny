using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StoryText : MonoBehaviour
{


    //textbox
    public GameObject textBox;

    public TextMeshProUGUI theText;

    //text to display
    public TextAsset textFile;
    public string[] textLines;

    //what line currently on
    public int currLine;

    //what line to stop reading
    public int endLine;

    //player
    public PlayerMovement player;
    private Rigidbody2D speechBubble;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        //speech bubbles rb
        speechBubble = GetComponent<Rigidbody2D>();


        if(textFile != null){
            textLines = (textFile.text.Split('\n'));
        }

        
        if(endLine == 0){
            endLine = textLines.Length-1;
        }

        nextLine();

    }

    // Update is called once per frame
    void Update()
    {
        //line to display
        //Debug.Log("1: " + textLines[currLine]);
        //theText.text = textLines[currLine];
        //Debug.Log("2: " + theText.text);
       // Vector2 velocity = new Vector2(player.speed, player.rb.velocity.y);
        speechBubble.velocity = player.rb.velocity;



        if(Input.GetKeyDown(KeyCode.Space)){
            currLine++;
            nextLine();
        }

        



    }

    private void nextLine(){


        if(currLine > endLine){
            textBox.SetActive(false);
        }else{
            theText.text = textLines[currLine];
        }
        

    }
}
