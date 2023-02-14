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
    public GameObject muzzleFlash;
    public float muzzleFlashTime = 0.1f;
    private float flashTimer;
    
    //sound that plays when shot still onCooldown;
    public AudioClip shotOnCooldown;

    // position of the mouse
    private Vector3 mousePos;
    // cooldown variables
    private float cooldownLeft = 0;
    public float recoilRot = 55f;
    private float gunRot = 0f;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // assigning references
        player = GetComponent<Player>();
        mainCam = Camera.main.GetComponent<Camera>();
        audioSource = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isAllowedMovement() && player.gun != null){

            if(cooldownLeft > 0f)
            {
                // decrease cooldown
                cooldownLeft -= Time.deltaTime;
                // recoil control
                gunRot -= ((cooldownLeft/player.gun.fireRate) * recoilRot) * Time.deltaTime;
                updateGunRot();
                // if(player.isFacingRight()){
                //     player.gun.transform.localRotation = Quaternion.Euler(0, 0, gunRot);
                // }else{
                //     player.gun.transform.localRotation = Quaternion.Euler(180f, 0, 360-gunRot);
                // }
            }else if(gunRot > 0){
                gunRot = 0f;
                updateGunRot();
            }

            if(flashTimer > 0){
                flashTimer -= Time.deltaTime;
                if(flashTimer <= 0f){
                    muzzleFlash.SetActive(false);
                }
            }

             // fire
                if (cooldownLeft <= 0f && (Input.GetMouseButtonDown(0))){
                    player.setShooting(true);// set player state
                    Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);// shoot bullet
                    audioSource.PlayOneShot(player.gun.gunshotSound, 0.13f);
                    cooldownLeft = player.gun.fireRate;// reset weapon cooldown
                    //kickback
                    gunRot = recoilRot;
                    // muzzle flash
                    muzzleFlash.SetActive(true);
                    flashTimer = muzzleFlashTime;
                }

                else if (cooldownLeft > 0f && Input.GetMouseButtonDown(0)){
                    audioSource.PlayOneShot(shotOnCooldown, 0.5f);
                }

            // enter/exit aiming
            if(player.gun != null && !player.isClimbing() && !player.isPushing() && player.getCombatTimer() > 0) { 
                player.setAiming(true);
            } 
            else{
                player.setAiming(false);
            }

            
            // while aiming
            if(player.isAiming())
            {
                // update mouse position
                mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

                // calculate rotation of mouse position from player
                Vector3 rotation = mousePos - transform.GetChild(0).position;
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

            }
        }
        
    }

    private void updateGunRot(){
        if(player.isFacingRight()){
            player.gun.transform.localRotation = Quaternion.Euler(0, 0, gunRot);
        }else{
            player.gun.transform.localRotation = Quaternion.Euler(180f, 0, gunRot);
        }
    }

    public float GetCooldownLeft(){
            return cooldownLeft;
    }
}
