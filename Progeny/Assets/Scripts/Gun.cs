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
    public AudioClip gunshotSound;
    public AudioClip reloadSound;

    private ThoughtBubble bubble;

    void Start(){
        ammoLeft = ammoCapacity;
        bubble =  GameObject.FindWithTag("ThoughtBubble").GetComponent<ThoughtBubble>();
    }

    void Update(){
        if(ammoLeft == 0){
            bubble.SetBubbleText("i need to reload ('R')");
            bubble.ShowBubbleForSeconds(2);
        }
    }
}
