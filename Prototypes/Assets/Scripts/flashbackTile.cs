using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class flashbackTile : MonoBehaviour
{

    public GameObject greenSky;
    public GameObject orangeSky;
    public GameObject greenClouds;
    public GameObject orangeClouds;
    public GameObject greenTrees;
    public GameObject orangeTrees;
    public GameObject greenHouse;
    public GameObject orangeHouse;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started waiting...");
        orangeClouds.SetActive(false);
        orangeTrees.SetActive(false);
        orangeHouse.SetActive(false);
        orangeSky.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D tile){
        if(tile.tag == "Player"){
            Debug.Log("on tileeee");
            orangeClouds.SetActive(true);
            orangeTrees.SetActive(true);
            orangeHouse.SetActive(true);
            orangeSky.SetActive(true);

            greenClouds.SetActive(false);
            greenTrees.SetActive(false);
            greenHouse.SetActive(false);
            greenSky.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D tile){
        if(tile.tag == "Player"){
            Debug.Log("leaving tile");
            orangeClouds.SetActive(false);
            orangeTrees.SetActive(false);
            orangeHouse.SetActive(false);
            orangeSky.SetActive(false);

            greenClouds.SetActive(true);
            greenTrees.SetActive(true);
            greenHouse.SetActive(true);
            greenSky.SetActive(true);
        }
    }

   
}
