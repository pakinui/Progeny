using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPlayerHouse : MonoBehaviour
{
    
    //the display to be hovered once interactable
    public GameObject display;

    private bool isTriggered = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            display.SetActive(true);
            Debug.Log("enter");
            isTriggered = true;
        }

    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.tag == "Player"){
            display.SetActive(false);
            isTriggered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTriggered){
            if(Input.GetKeyDown("e")){
                SceneManager.LoadScene("LevelDone");
            }
        }
    }
}
