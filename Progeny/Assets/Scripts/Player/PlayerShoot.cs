using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // reference to the 'Player' script
    private Player player;

    // reference to the main camera
    private Camera mainCam;
    // reference to the bullet object
    public GameObject bullet;
    // reference to the rotation point
    public GameObject pointOfRotation;
    // reference to the bullet spawn point (end of barrel)
    public Transform bulletSpawnPoint;
    // position of the mouse
    private Vector3 mousePos;
    // cooldown variables
    private float cooldownLeft = 0;
    private float reloadLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
        mainCam = Camera.main.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAllowedMovement()){

            if(cooldownLeft > 0f)
        {
            // decrease cooldown
            cooldownLeft -= Time.deltaTime;
        }

        // enter/exit aiming
        if(player.gun != null && Input.GetMouseButtonDown(1) && !player.isClimbing() && !player.isPushing()) { 
            player.setAiming(true);
        } else if(player.isAiming() && Input.GetMouseButtonUp(1)) {
            player.setAiming(false);
        }

        // enter reload
        if(player.gun != null && player.gun.ammoLeft < player.gun.ammoCapacity && Input.GetKeyDown("r") && !player.isReloading())
        {
            player.setReloading(true);
        }
        // countdown reload
        if(player.isReloading())
        {
            reloadLeft -= Time.deltaTime;
            if(reloadLeft <= 0f){
                player.gun.ammoLeft = player.gun.ammoCapacity;
                player.setReloading(false);
            }
        }

        
        // while aiming
        if(player.isAiming())
        {
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
            if(rotZ > -60 || rotZ < -120)
            {
                transform.GetChild(0).rotation = Quaternion.Euler(0,0,rotZ);
            }

            // check weapon cooldown
            if(cooldownLeft > 0f)
            {
                // decrease cooldown
                cooldownLeft -= Time.deltaTime;
            }

            //fire
            else if(cooldownLeft <= 0f && Input.GetMouseButtonDown(0) && player.gun.ammoLeft > 0){
                player.setShooting(true);// set player state
                Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);// shoot bullet
                player.gun.ammoLeft--;// decrease ammo
                cooldownLeft = player.gun.fireRate;// reset weapon cooldown
            }
        }
        }
        

    }

   public float GetCooldownLeft(){
        return cooldownLeft;
   }
}
