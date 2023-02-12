using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

   // An array with the sprites used for animation
   public Sprite idleSprite;
   public Sprite fallingSprite;
   public Sprite crouchIdleSprite;
   public Sprite[] walkingSprites;
   public Sprite[] walkingShoot;
   public Sprite[] crouchWalkingSprites;
   public Sprite[] crouchingShoot;
   public Sprite[] pushingSprites;
   public Sprite[] climbingSprites;
   public Sprite[] shortClimbSprites;
   public Sprite[] meleeSprites;
   public Sprite[] reloadSprites;

   //set to whatever action player is doing atm
   private Sprite[] animSprites;
   
   // Controls how fast to change the sprites when
   // animation is running
   public float framesPerSecond;
   // saves FPS value to allow for state specific adjustments
   private float originalFPS;
   
   // Reference to the renderer of the sprite
   // game object
   SpriteRenderer animRenderer;
   
   // Time passed since the start of animation
   private float timeAtAnimStart;
   
   // Indicates whether animation is running or not
   private bool animRunning = false;

   //is player currently walking
   private Player player;


   //player bottom left coords
   private float playerX;
   private float playerY;

   
   
   // Use this for initialization
   void Start () {
      // Get a reference to game object renderer and
      // cast it to a Sprite Rendere
      animRenderer = GetComponent<Renderer>() as SpriteRenderer;
      animSprites = walkingSprites;

      //reference to player
      player = GetComponent<Player>();

      // set default frames per second;
      originalFPS = framesPerSecond;
   }

   // At fixed time intervals...
   void FixedUpdate () {
      if(!animRunning) {
         // The animation is triggered by user input
         float userInput = Input.GetAxis("Horizontal");
         if(userInput != 0f || !player.isAllowedMovement()) {
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
            // reset the frames per second
            framesPerSecond = originalFPS;

            // state specific animations
            if(player.isPushing()){
               animSprites = pushingSprites;
            }else if(player.isCrouching()){
               framesPerSecond /= 2;
               animSprites = crouchWalkingSprites;
            }else if(player.isClimbing()){

               animSprites = climbingSprites;
               
               
            }else{
               animSprites = walkingSprites;
            }
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
         if(player.isCrouching()){
            animRenderer.sprite = crouchIdleSprite;
         }else{
            animRenderer.sprite = idleSprite;
         }
      }

         
   }


   public void flipPlayer(){
      player.Flip();
   }

   //methods to set movement during cutscenes
   public void setPlayerMovementTrue(){
      player.setMoving(true);
   }

    public void setPlayerMovementFalse(){
      player.setMoving(false);
   }
}