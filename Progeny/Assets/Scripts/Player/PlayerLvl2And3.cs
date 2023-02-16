using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvl2And3 : MonoBehaviour
{
    //the display to be hovered once interactable
    private Player player;
    public GameObject gun;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        // assign reference to gun in the Player script
            GameObject newGun = Instantiate(gun);
            player.gun = newGun.GetComponent<Gun>();
            // hide gun
            newGun.SetActive(false);
            // give the gun to the player object and move position
            newGun.transform.parent = player.transform.GetChild(0);
            newGun.transform.localPosition = new Vector3(0.55f, 0f, -1f);
            // rotate if necessary
            if(!player.isFacingRight()){
                newGun.transform.Rotate(0f, 0f, 180f);
            }
            // assign reference to the bulletSpawnPoint
            player.gameObject.GetComponent<PlayerShoot>().bulletSpawnPoint = newGun.transform.GetChild(0);
            // assign reference to muzzleFlash
            player.gameObject.GetComponent<PlayerShoot>().muzzleFlash = newGun.transform.GetChild(1).gameObject;
            newGun.transform.GetChild(1).gameObject.SetActive(false);
    }
}
