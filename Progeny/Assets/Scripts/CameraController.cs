using UnityEngine;

namespace Ruiyi.Controller.GameController
{
    public class CameraController : MonoBehaviour
    {
        public float smoothSpeed = 2f;
        public float yOffset = 2f;
        public float xOffset = 2f;
        public Transform player;

        // private float xMin = -5;
        // private float xMax = 5;
        // private float yMin = -5;
        // private float yMax = 5;

        private Vector3 targetPos;
        private Vector3 playerPos;

        void Start()
        {
            player = GameObject.Find("Player").transform;
            playerPos = player.position;
            targetPos = new Vector3(playerPos.x + xOffset, playerPos.y + yOffset, -10f);
        }

        void FixedUpdate()
        {
            float isRight = Mathf.Sign(player.localScale.x);

            Vector3 playerPos = player.position;
            targetPos.x = (playerPos.x + xOffset) * isRight;
            targetPos.y = playerPos.y + yOffset;

            // update camera position
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothSpeed);
        }
    }
}