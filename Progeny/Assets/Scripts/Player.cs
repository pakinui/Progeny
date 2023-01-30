using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player states
    private bool isMoving, isCrouching, isClimbing, isShooting, isReloading = false;

    // movement speed multiplier
    // will increase/decrease depending on player state
    public float movementSpeed = 2f;
    // direction of player.
    private bool facingRight = true;
    // reference to the RigidBody component
    private Rigidbody2D rb;
    // reference to the main camera object
    private Camera mainCam;
    // position of the mouse
    private Vector3 mousePos;
    // reference to the bullet object
    public GameObject bullet;
    // reference to the bullet spawn point (end of barrel)
    public Transform bulletSpawnPoint;

    private bool canShoot = true;
    private float timeSinceLastShot;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if(Mathf.Abs(rotZ) > 90 && facingRight)
        {
            Flip();
        }
        else if(Mathf.Abs(rotZ) < 90 && !facingRight)
        {
            Flip();
        }
        // rotate gun
        transform.GetChild(0).rotation = Quaternion.Euler(0,0,rotZ);

        // horizontal movement input
        float horizontalAxis = Input.GetAxis("Horizontal");

        // horizontal movement
        if(!isClimbing && horizontalAxis != 0)
        {
            isMoving = true;
            rb.velocity = new Vector2(horizontalAxis * movementSpeed, rb.velocity.y);
        }
        else
        {
            isMoving = false;
        }

    }

    // method to flip the Player object and update the direction variable
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        transform.GetChild(0).Rotate(0f, 0f, 180f);
    }

}
