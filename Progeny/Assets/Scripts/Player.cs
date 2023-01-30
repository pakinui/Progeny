using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player states
    public bool isMoving, isCrouching, isClimbing, isAiming, isShooting, isReloading = false;
    // direction of player.
    public bool facingRight = true;

    // Start is called before the first frame update
    static void Start(){}

    // Update is called once per frame
    static void Update(){}

    // method to flip the Player object and update the direction variable
    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
