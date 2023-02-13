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
    
    //sound that plays when shot still onCooldown;
    public AudioClip shotOnCooldown;
    //sound that plays when there is no ammo
    public AudioClip noAmmo;
    // position of the mouse
    private Vector3 mousePos;
    // cooldown variables
    private float cooldownLeft = 0;
    private float reloadLeft = 0;
    public float recoilRot = 70f;
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
        if(player.isAllowedMovement()){

            if(cooldownLeft > 0f)
            {
                // decrease cooldown
                cooldownLeft -= Time.deltaTime;
                // recoil control
                gunRot -= ((cooldownLeft/player.gun.fireRate) * recoilRot) * Time.deltaTime;
                if(player.isFacingRight()){
                    player.gun.transform.localRotation = Quaternion.Euler(player.gun.transform.rotation.x, 0, gunRot);
                }else{
                    player.gun.transform.localRotation = Quaternion.Euler(player.gun.transform.rotation.x, 0, 360-gunRot);
                }
            }else if(gunRot > 0){
                gunRot = 0f;
                player.gun.transform.localRotation = Quaternion.Euler(player.gun.transform.rotation.x, 0, 0);
            }

            // enter/exit aiming
            if(player.gun != null && (Input.GetKey(KeyCode.LeftShift)) && !player.isClimbing() && !player.isPushing()) { 
                player.setAiming(true);
            } else if(player.isAiming() && !Input.GetKey(KeyCode.LeftShift)) {
                player.setAiming(false);
            }

            // enter reload
            if(player.gun != null && player.gun.ammoLeft < player.gun.ammoCapacity && Input.GetKeyDown("r") && !player.isReloading())
            {
                player.setReloading(true);
                reloadLeft = player.gun.reloadTime;
                audioSource.PlayOneShot(player.gun.reloadSound, 0.5f);
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

                // check weapon cooldown
                // if(cooldownLeft > 0f)
                // {
                //     // decrease cooldown
                //     cooldownLeft -= Time.deltaTime;
                // }


                    //fire
                if (cooldownLeft <= 0f && (Input.GetMouseButtonDown(0)) && player.gun.ammoLeft > 0){
                    player.setShooting(true);// set player state
                    Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);// shoot bullet
                    audioSource.PlayOneShot(player.gun.gunshotSound, 0.13f);
                    player.gun.ammoLeft--;// decrease ammo
                    cooldownLeft = player.gun.fireRate;// reset weapon cooldown
                    //kickback
                    gunRot = recoilRot;
                }

                else if (Input.GetMouseButtonDown(0) && player.gun.ammoLeft <= 0){
                    audioSource.PlayOneShot(noAmmo, 0.9f);
                }

                else if (cooldownLeft > 0f && Input.GetMouseButtonDown(0)){
                    audioSource.PlayOneShot(shotOnCooldown, 0.5f);
                }
            }
        }
        
    }

   public float GetCooldownLeft(){
        return cooldownLeft;
   }
}
