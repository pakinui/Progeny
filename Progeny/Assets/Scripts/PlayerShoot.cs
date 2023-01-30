using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // reference to the 'Player' script
    Player player;
    // reference to the main camera object
    private Camera mainCam;
    // reference to the bullet object
    public GameObject bullet;
    // reference to the bullet spawn point (end of barrel)
    public Transform bulletSpawnPoint;
    // position of the mouse
    private Vector3 mousePos;
    // cooldown variables
    private bool canShoot = true;
    private float timeSinceLastShot;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // update mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); 
        // calculate suitable rotation
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        // flip Player object if suitable
        if(Mathf.Abs(rotZ) > 90 && player.facingRight)
        {
            player.Flip();
        }
        else if(Mathf.Abs(rotZ) < 90 && !player.facingRight)
        {
            player.Flip();
        }
        // rotate gun
        transform.GetChild(0).rotation = Quaternion.Euler(0,0,rotZ);
    }
}
