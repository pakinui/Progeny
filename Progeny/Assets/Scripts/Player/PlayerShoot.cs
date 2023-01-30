using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // reference to the 'Player' script
    Player player;

    // reference to the main camera
    private Camera mainCam;
    // reference to the bullet object
    public GameObject bullet;
    // reference to the weapon object
    public GameObject weapon;
    // reference to the rotation point
    public GameObject pointOfRotation;
    // reference to the bullet spawn point (end of barrel)
    public Transform bulletSpawnPoint;
    // position of the mouse
    private Vector3 mousePos;
    // cooldown variables
    public float cooldown = 3f;

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
        // enter/exit aiming
        if(Input.GetMouseButtonDown(1)){
            player.setAiming(true);
            weapon.SetActive(true);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            player.setAiming(false);
            weapon.SetActive(false);
        }

        // if aiming
        if(player.isAiming()){
            // update mouse position
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            // calculate rotation of mouse position from player
            Vector3 rotation = mousePos - transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            // flip player if suitable
            if(Mathf.Abs(rotZ) > 90 && player.isFacingRight())
            {
                player.Flip();
            }
            else if(Mathf.Abs(rotZ) < 90 && !player.isFacingRight())
            {
                player.Flip();
            }

            // rotate (parent object of) gun
            transform.GetChild(0).rotation = Quaternion.Euler(0,0,rotZ);

            // check weapon cooldown
            if(cooldown > 0f)
            {
                // decrease cooldown
                cooldown -= Time.deltaTime;
            }
            else if(cooldown <= 0 && Input.GetMouseButtonDown(0)){
                // shoot and reset weapon cooldown
                Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                cooldown = 3f;
            }
        }
    }
}
