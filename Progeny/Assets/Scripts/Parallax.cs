using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Parallax makes the background move is comparison to
    how far away it is from the player.
*/
public class Parallax : MonoBehaviour
{
    // reference to the main camera
    public Camera cam;
    // reference to the player
    public Transform player;

    Vector2 startPosition;

    float startZ;

    Vector2 travel => (Vector2)cam.transform.position - startPosition;

    float distanceFromPlayer => transform.position.z - player.position.z;

    float clippingPlane => (cam.transform.position.z + (distanceFromPlayer > 0 ? cam.farClipPlane : cam.nearClipPlane));
    
    float parallaxFactor => Mathf.Abs(distanceFromPlayer) / clippingPlane;

    public void Start(){
        // 
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    public void Update(){
        // 
        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}

