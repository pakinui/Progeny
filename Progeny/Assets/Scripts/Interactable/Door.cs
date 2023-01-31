using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject displayPrefab;
    private GameObject display;
    private bool isUsable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsable && Input.GetKeyDown("e")){
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            isUsable = true;
            display = Instantiate(displayPrefab, this.transform.parent);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            isUsable = false;
            Destroy(display);
        }
    }

}
