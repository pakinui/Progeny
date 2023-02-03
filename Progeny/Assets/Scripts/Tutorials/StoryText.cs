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

    //is the story currently playing
    public bool storyInAction = false;
    public  bool storyComplete = false;
    //private Vector3 tempSpeed;

    private ThoughtBubble thought;


    void OnEnable(){
        storyComplete = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        textBox.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        thought = GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();

    }

    void NewStory(){
        textLines = null;
        storyInAction = false;
        storyComplete = false;
        currLine = 0;
        endLine = 0;
    }

    public void PlayStoryText(TextAsset file){
        NewStory();
        storyComplete = false;
        if(thought.showBubble) thought.hideBubble();

        if(file != null){
            textLines = (file.text.Split('\n'));
        }else{
            Debug.Log("file is null");
        }

        
        if(endLine == 0){
            endLine = textLines.Length-1;
        }
        StartStoryText();
    }


    void StartStoryText(){
        
        storyInAction = true;
        //textBox.SetActive(true);
        textBox.SetActive(!textBox.activeSelf);
        nextLine();

    }

    // Update is called once per frame
    void Update()
    {
        if(storyInAction){
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse2)){
                currLine++;
                nextLine();
            }
        }
        

    }

    private void nextLine(){


        if(currLine > endLine){
            textBox.SetActive(false);
            player.startPlayerMovement();
            storyComplete = true;
            storyInAction = false;
        }else{
            theText.text = textLines[currLine];
        }
        

    }
}
