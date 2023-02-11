using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //the display to be hovered once interactable
    public GameObject display;

    //private reference to said display
    //private GameObject display;
    //interactable flag
    private bool isInteractable = false;

    // reference to the player script
    private Player player;

    // reference to the gun to pickup
    public GameObject newGun;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && Input.GetKeyDown("e")){
            // assign reference to gun in the Player script
            player.gun = newGun.GetComponent<Gun>();
            // hide gun
            newGun.SetActive(false);
            // give the gun to the player object and move position
            newGun.transform.parent = player.transform.GetChild(0);
            newGun.transform.localPosition = new Vector3(0.75f, 0f, -1f);
            // rotate if necessary
            if(!player.isFacingRight()){
                newGun.transform.Rotate(0f, 0f, 180f);
            }
            // assign reference to the bulletSpawnPoint
            player.gameObject.GetComponent<PlayerShoot>().bulletSpawnPoint = newGun.transform.GetChild(0);
            player.gotGun();
            // destroy the pickup object
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            isInteractable = true;
            display.SetActive(true);
            //display = Instantiate(displayPrefab, this.transform.parent);

            //translates display up 2.5
            //display.transform.localPosition = new Vector3(0, 2.5f, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player"){
            isInteractable = false;
            //Destroy(display);
            display.SetActive(false);
        }
    }

}
