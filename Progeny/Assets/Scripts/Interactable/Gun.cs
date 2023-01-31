using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //the display to be hovered once interactable
    public GameObject displayPrefab;

    //private reference to said display
    private GameObject display;

    //interactable flag
    private bool isInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && Input.GetKeyDown("e")){
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            isInteractable = true;
            display = Instantiate(displayPrefab, this.transform.parent);

            //translates display up 2.5
            display.transform.localPosition = new Vector3(0, 2.5f, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player"){
            isInteractable = false;
            Destroy(display);
        }
    }

}
