using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //the display to be hovered once interactable
    public GameObject display;
    public GunBlock block;
    //private reference to said display
    //private GameObject display;
    //interactable flag
    private bool isInteractable = false;

    // reference to the player script
    private Player player;

    // reference to the gun to pickup
    public GameObject newGun;

    //checks if firstLevel for the purpose of blocking the player if they don't have a gun
    public bool firstLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteractable && Input.GetKeyDown("e")){
            if (firstLevel){
                block.gun = true;
            }
            // assign reference to gun in the Player script
            player.gun = newGun.GetComponent<Gun>();
            // hide gun
            newGun.SetActive(false);
            // give the gun to the player object and move position
            newGun.transform.parent = player.transform.GetChild(0);
            newGun.transform.localPosition = new Vector3(0.60f, 0f, -1f);
            // rotate if necessary
            if(!player.isFacingRight()){
                newGun.transform.Rotate(0f, 0f, 180f);
            }
            // assign reference to the bulletSpawnPoint
            player.gameObject.GetComponent<PlayerShoot>().bulletSpawnPoint = newGun.transform.GetChild(0);
            // assign reference to muzzleFlash
            player.gameObject.GetComponent<PlayerShoot>().muzzleFlash = newGun.transform.GetChild(1).gameObject;
            newGun.transform.GetChild(1).gameObject.SetActive(false);
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
