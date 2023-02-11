using UnityEngine;
using System.Collections;
//copy and pasted from animation lab

public class StandardEnemyController : MonoBehaviour {

   // An array with the sprites used for animation
   
   public Sprite[] idleSprites;
   public Sprite[] approachSprites;
   public Sprite[] pouncePrepSprites;
   public Sprite[] pounceSprites;
   public Sprite[] deathSprites;
   
   private Sprite[] currSprites;//current sprites playing

   // Controls how fast to change the sprites when
   // animation is running
   public float framesPerSecond;
   
   // Reference to the renderer of the sprite
   // game object
   SpriteRenderer animRenderer;
   
   // Time passed since the start of animation
   private float timeAtAnimStart;
   
   // Indicates whether animation is running or not
   private bool animRunning = false;

   private GroundEnemy ground;

   private bool deathAnimation = false;//if currently doing death animation

   //stuff to check if last frame is apart of the same animation
   private bool idle = false;
   private bool approach = false;
   private bool pouncePrep = false;
   private bool pounce = false;
   private bool death = false;
   
   // Use this for initialization
   void Start () {
      // Get a reference to game object renderer and
      // cast it to a Sprite Rendere
      animRenderer = GetComponent<Renderer>() as SpriteRenderer;
      ground = GetComponent<GroundEnemy>();

      // Animation will start playing
      animRunning = true;
            
      // Record time at animation start
      timeAtAnimStart = Time.timeSinceLevelLoad;

     
   }

   

   // Before rendering next frame...
   void Update () {

      if(animRunning) {
         // Animation is running, so we need to 
         // figure out what frame to use at this point
         // in time

         if(ground.state == GroundEnemy.State.Idle && !idle){
            
               currSprites = idleSprites;
               idle = true;
               timeAtAnimStart = Time.timeSinceLevelLoad;
            
            
            //frameIndex = 0;
         }else if(ground.state == GroundEnemy.State.Approach   || ground.state == GroundEnemy.State.Dash  || ground.state == GroundEnemy.State.DashPrep){
            if(!approach){
               currSprites = approachSprites;
            approach = true;
            idle = false;
            pouncePrep = false;
            timeAtAnimStart = Time.timeSinceLevelLoad;
            }
            
            //frameIndex = 0;
         }else if(ground.state == GroundEnemy.State.PouncePrep && !pouncePrep){
            currSprites = pounceSprites;
            pouncePrep = true;
            approach = false;
            timeAtAnimStart = Time.timeSinceLevelLoad;
            //frameIndex = 0;
         }
         // }else if(ground.state == GroundEnemy.State.Pounce && !pounce){
         //    currSprites = pounceSprites;
         //    pounce = true;
         //    pouncePrep = false;
         //    timeAtAnimStart = Time.timeSinceLevelLoad;
         //    //frameIndex = 0;
         // }
         
         // Compute number of seconds since animation started playing
         float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
         
         // Compute the index of the next frame    
         int frameIndex = (int) (timeSinceAnimStart * framesPerSecond);
         
         if(frameIndex < currSprites.Length) {
            // Let the renderer know which sprite to
            // use next      
            animRenderer.sprite = currSprites[ frameIndex ];
         } else {
            // Animation finished, set the render
            // with the first sprite and stop the 
            // animation
            animRenderer.sprite = currSprites[0];
            // animRunning = false;
         }
      }   
   }
}