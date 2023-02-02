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
    private Player player;
    // private Rigidbody2D speechBubble;

    public  bool storyComplete = false;
    //private Vector3 tempSpeed;


    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        //player = FindObjectOfType<PlayerMovement>();
        //speech bubbles rb
        //speechBubble = GetComponent<Rigidbody2D>();


        if(textFile != null){
            textLines = (textFile.text.Split('\n'));
        }

        
        if(endLine == 0){
            endLine = textLines.Length-1;
        }

        

    }

    public void PlayStoryText(TextAsset file){

        if(file != null){
            textLines = (file.text.Split('\n'));
        }

        
        if(endLine == 0){
            endLine = textLines.Length-1;
        }
        StartStoryText();
    }

    // void OnTriggerEnter2D(Collider2D collider){

    //     if(collider.tag == "Player" && !storyComplete){
    //         StartStoryText();
    //         storyComplete = true;
    //     }
    // }

    void StartStoryText(){
        
        //stop player from being able to move
        // player.setAllowedMovement(false);
        // player.setMoving(false);
        textBox.SetActive(true);
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
        // speechBubble.velocity = player.rb.velocity;



        if(Input.GetKeyDown(KeyCode.Space)){
            currLine++;
            nextLine();
        }

        



    }

    private void nextLine(){


        if(currLine > endLine){
            textBox.SetActive(false);
            player.setAllowedMovement(true);
            player.setMoving(true);
            storyComplete = true;
        }else{
            theText.text = textLines[currLine];
        }
        

    }
}
