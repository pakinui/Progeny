using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

   // An array with the sprites used for animation
   public Sprite[] animSprites;
   public Sprite idleSprite;
   
   // Controls how fast to change the sprites when
   // animation is running
   public float framesPerSecond;
   
   // Reference to the renderer of the sprite
   // game object
   SpriteRenderer animRenderer;
   
   // Time passed since the start of animatin
   private float timeAtAnimStart;
   
   // Indicates whether animation is running or not
   private bool animRunning = false;

   //is player currently walking
   private Player player;
   
   // Use this for initialization
   void Start () {
      // Get a reference to game object renderer and
      // cast it to a Sprite Rendere
      animRenderer = GetComponent<Renderer>() as SpriteRenderer;

      //reference to player
      player = GetComponent<Player>();
   }

   // At fixed time intervals...
   void FixedUpdate () {
      if(!animRunning) {
         // The animation is triggered by user input
         float userInput = Input.GetAxis("Horizontal");
         if(userInput != 0f) {
            // User pressed the move left or right button
            
            // Animation will start playing
            animRunning = true;
            
            // Record time at animation start
            timeAtAnimStart = Time.timeSinceLevelLoad;
         }
      }
   }

   // Before rendering next frame...
   void Update () {

      if(player.isMoving()){
         if(animRunning) {
         // Animation is running, so we need to 
         // figure out what frame to use at this point
         // in time
         
         // Compute number of seconds since animation started playing
         float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
         
         // Compute the index of the next frame    
         int frameIndex = (int) (timeSinceAnimStart * framesPerSecond);
         
         if(frameIndex < animSprites.Length) {
            // Let the renderer know which sprite to
            // use next      
            animRenderer.sprite = animSprites[ frameIndex ];
         } else {
            // Animation finished, set the render
            // with the first sprite and stop the 
            // animation
            animRenderer.sprite = animSprites[0];
            animRunning = false;
         }
      }
      }else{
            animRenderer.sprite = idleSprite;
      }

         
   }
}