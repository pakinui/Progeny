using UnityEngine;

namespace Ruiyi.Controller.GameController
{
    public class CameraController : MonoBehaviour
    {
        // reference to the player
        public Transform player;
        // reference to the position of the player
        private Vector3 playerPos;
        // reference to the targetted position for the camera
        private Vector3 targetPos;

        // speed at which the camera follows the player
        public float smoothSpeed = 4f;
        // offset of camera position
        public float xOffset = 3f;
        public float yOffset = 3.25f;

        // private float xMin = -5;
        // private float xMax = 5;
        // private float yMin = -5;
        // private float yMax = 5;

        // Start is called before the first frame update
        void Start()
        {
            // assigning references
            player = GameObject.Find("Player").transform;
            playerPos = player.position;
            targetPos = new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, -10f);
        }

        // FixedUpdate is called once per fixed frame-rate frame
        void FixedUpdate()
        {
            // direction determined variable
            float isRight = Mathf.Sign(player.localScale.x);

            // reassigning references
            playerPos = player.position;
            targetPos.x = (playerPos.x + xOffset) * isRight;
            targetPos.y = playerPos.y + yOffset;

            // update camera position
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
        }
    }
}