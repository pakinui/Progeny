using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage; // how much damage is dealt per hit
    public float fireRate; // time between shots (cooldown)
    public int ammoCapacity; // shots between reload
    public float reloadTime; // time taken to reload

    void Start(){}
}
