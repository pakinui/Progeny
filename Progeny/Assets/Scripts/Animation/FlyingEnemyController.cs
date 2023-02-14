using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    // Reference to the renderer of the sprite game object
    SpriteRenderer animRenderer;
    SpriteRenderer wingRenderer;

    // sprites
    public Sprite[] wingSprites;
    public Sprite[] deathSprites;

    // Controls how fast to change the sprites when animation is running
    public float framesPerSecond;
    public float wingFPS;
    
    // Time passed since the start of animation
    private float timeAtAnimStart;


    // Start is called before the first frame update
    void Start()
    {
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        wingRenderer = transform.GetChild(0).GetComponent<Renderer>() as SpriteRenderer;

        // Record time at animation start
        timeAtAnimStart = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        // Compute number of seconds since animation started playing
        float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;

        // Compute the index of the next frame    
        int wingFrameIndex = (int) (timeSinceAnimStart * wingFPS);
        wingRenderer.sprite = wingSprites[wingFrameIndex % wingSprites.Length];
        
    }
}
