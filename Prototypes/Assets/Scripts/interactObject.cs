using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactObject : MonoBehaviour
{

    public GameObject gun;
    private bool nearGun = false;
    public GameObject floorGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nearGun){
            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("picked up gun");
                gun.SetActive(true);
                floorGun.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        nearGun = true;
    }

    void OnTriggerExit2D(Collider2D col){
        nearGun= false;
    }
}
