using UnityEngine;

//namespace Ruiyi.Controller.GameController
//{
    public class CameraController : MonoBehaviour
    {
        // reference to the player
        private Player player;
        // reference to the player's rigid body
        private Rigidbody2D rb;
        // reference to the position of the player
        private Vector3 playerPos;
        // reference to the targetted position for the camera
        private Vector3 targetPos;

        // speed at which the camera follows the player
        public float smoothSpeed;
        private float camSpeed; // smooth speed when out of combat
        // movement direction determined variable. +right, -left
        float direction;
        // offset of camera position    
        public float xOffset = 3f;
        public float yOffset = 3.5f;

        public float leftEdgeX = -17.97f;
        public float rightEdgeX = 189.62f;
        
        private bool reduceVolume = false;
        private float changeDuration = 0;
        private float muteDuration = 0;

        private AudioSource audioSource;
        // private float xMin = -5;
        // private float xMax = 5;
        // private float yMin = -5;
        // private float yMax = 5;

        // Start is called before the first frame update
        void Start()
        {
            // assigning references
            player = GameObject.Find("Player").GetComponent<Player>();
            rb = player.gameObject.GetComponent<Rigidbody2D>();
            playerPos = player.transform.position;
            targetPos = new Vector3(playerPos.x + xOffset, yOffset-3, -10f);
            audioSource = GetComponent<AudioSource>();
        }


        void Update(){
            if (muteDuration >= 0){
                muteDuration -= Time.deltaTime;
            }
            if (reduceVolume && audioSource.volume > 0){
                audioSource.volume -= Time.deltaTime / changeDuration;
            }
            else if (!reduceVolume && audioSource.volume < 1 && muteDuration <= 0){
                audioSource.volume += Time.deltaTime / changeDuration;
            }
        }

        // FixedUpdate is called once per fixed frame-rate frame
        void FixedUpdate()
        {
            // reassigning player position
            playerPos = player.transform.position;

            if(!player.isShooting() && !player.isHitting() && player.isAllowedMovement()){
                camSpeed = smoothSpeed;
                // reassigning the movement direction vairiable
                if(Input.GetAxis("Horizontal") > 0){
                    direction = 1;
                }else if(Input.GetAxis("Horizontal") < 0){
                    direction = -1;
                }
            } else {
                // change variables if player is in combat
                direction = 0;
                // keeps up with player without being too quick
                camSpeed = (player.outOfCombatDuration - player.getCombatTimer()); 
            }
            
            // reassigning target camera position variables
            targetPos.x = playerPos.x + (xOffset * direction);

            // update camera position
            if(targetPos.x < leftEdgeX){
                targetPos.x = leftEdgeX;
            }
            if(targetPos.x > rightEdgeX){
                targetPos.x = rightEdgeX;
            }
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * camSpeed);
        }

        public void Mute(bool mute, float cDuration){
            if (mute){
                audioSource.volume = 1;
            }
            else{
                audioSource.volume = 0;
            }
            reduceVolume = mute;
            changeDuration = cDuration;
        }

        public void Mute(bool mute, float cDuration, float mDuration){
            if (mute){
                audioSource.volume = 1;
            }
            else{
                audioSource.volume = 0;
            }
            reduceVolume = mute;
            changeDuration = cDuration;
            muteDuration = mDuration;
        }
    }
//}