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
    private TextAsset textFile;
    private string[] textLines;

    //what line currently on
    private int currLine;

    //what line to stop reading
    private int endLine;

    //player
    private Player player;
    // private Rigidbody2D speechBubble;

    public  bool storyComplete = false;
    //private Vector3 tempSpeed;

    private ThoughtBubble thought;


    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

    }

    public void PlayStoryText(TextAsset file){

        if(thought.showBubble) thought.hideBubble();

        if(file != null){
            textLines = (file.text.Split('\n'));
        }

        
        if(endLine == 0){
            endLine = textLines.Length-1;
        }
        StartStoryText();
    }


    void StartStoryText(){
        

        textBox.SetActive(true);
        nextLine();

    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Space)){
            currLine++;
            nextLine();
        }

        



    }

    private void nextLine(){


        if(currLine > endLine){
            textBox.SetActive(false);
            player.startPlayerMovement();
            storyComplete = true;
        }else{
            theText.text = textLines[currLine];
        }
        

    }
}
