using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class FlashbackTiles : MonoBehaviour
{

    public GameObject green;
    public GameObject orange;


    // Start is called before the first frame update
    void Start()
    {
        orange.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D tile){
        if(tile.tag == "Player"){
            orange.SetActive(true);
            green.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D tile){
        if(tile.tag == "Player"){
            Debug.Log("leaving tile");
            // orangeClouds.SetActive(false);
            // orangeTrees.SetActive(false);
            // orangeHouse.SetActive(false);
            orange.SetActive(false);

            // greenClouds.SetActive(true);
            // greenTrees.SetActive(true);
            // greenHouse.SetActive(true);
            green.SetActive(true);
        }
    }

   
}
