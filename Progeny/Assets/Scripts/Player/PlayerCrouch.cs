﻿using System;
using UnityEngine;
using Utility;

public class PlayerCrouch : MonoBehaviour
{
    public float crouchDuration = 0.2f;
    public GameObject ceilingCheck;
    private TriggerCheck2D ceilingTriggerCheck2D;
    private bool crouchPressed;
    
    private Player player;
    private BoxCollider2D bc;
    
    public Vector3 targetColliderSize = new Vector3(0.45f, 1.25f, 0f);
    private Vector3 originalColliderSize;
    
    private float crouchTimer;
    private float zPos;

    void Start()
    {
        // Start is called before the first frame update
        player = GetComponent<Player>();
        ceilingTriggerCheck2D = ceilingCheck.GetComponent<TriggerCheck2D>();
        
        bc = GetComponent<BoxCollider2D>();
        originalColliderSize = bc.size;
        zPos = player.transform.position.z;
    }
    
    void BeginCrouch()
    {
        // set player state
        player.setCrouching(true);
        // decrease the player collider's size and adjust its position to avoid a short fall
        bc.size = new Vector3(targetColliderSize.x, targetColliderSize.y, zPos);
        //collider.size = Vector2.Lerp(originalColliderSize, targetColliderSize, crouchDuration);
        transform.position = new Vector3(transform.position.x, transform.position.y - ((originalColliderSize.y - targetColliderSize.y) / 2),zPos);
    }
    
    void EndCrouch()
    {
        // set player state
        player.setCrouching(false);
        // increase the player collider's size and adjust its position
        bc.size = new Vector3(originalColliderSize.x, originalColliderSize.y, zPos);
        transform.position = new Vector3(transform.position.x, transform.position.y + ((originalColliderSize.y - targetColliderSize.y) / 2), zPos);
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Crouch") || Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouchPressed = true;
        }
        else if (!Input.GetButton("Crouch") && !Input.GetKey(KeyCode.LeftControl))
        {
            crouchPressed = false;
        }
    }

    private void FixedUpdate()
    {
        
        if (player.isClimbing() || player.isPushing()) return;
        
        // Crouch
        if (!crouchPressed && !ceilingTriggerCheck2D.Triggered() && player.isCrouching())
        {
            // stand up
            crouchTimer -= Time.fixedDeltaTime;
            if (crouchTimer <= 0)
            {
                crouchTimer = 0;
                EndCrouch();
            }
        } 
        else if (crouchPressed && !player.isCrouching())
        {
            // crouch
            BeginCrouch();
            crouchTimer += Time.fixedDeltaTime;
            if (crouchTimer >= crouchDuration)
            {
                crouchTimer = crouchDuration;
            }
        }
    }
}