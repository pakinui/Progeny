using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage; // how much damage is dealt per hit
    public float fireRate; // time between shots (cooldown)
    public int ammoCapacity; // max number of rounds between reloads
    public int ammoLeft; // current number of rounds left
    public float reloadTime; // time taken to reload

    void Start(){
        ammoLeft = ammoCapacity;
    }
}
