using UnityEngine;

namespace Ruiyi.Controller.GameController
{
    public class CameraController : MonoBehaviour
    {
        public float smoothSpeed = 2f;
        public Transform mPlayerTrans;

        private float xMin = -5;
        private float xMax = 5;
        private float yMin = -5;
        private float yMax = 5;

        private Vector3 mTargetPos;

        void LateUpdate()
        {
            if (!mPlayerTrans)
            {
                var playerGameObj = GameObject.FindWithTag("Player");

                if (playerGameObj)
                {
                    mPlayerTrans = playerGameObj.transform;
                }
                else
                {
                    return;
                }
            }

            var isRight = Mathf.Sign(mPlayerTrans.localScale.x);

            var playerPos = mPlayerTrans.position;
            mTargetPos.x = playerPos.x + 3 * isRight;
            mTargetPos.y = playerPos.y + 2;
            mTargetPos.z = -10;

            var position = transform.position;

            position = Vector3.Lerp(position, mTargetPos, smoothSpeed * Time.deltaTime);
            
            transform.position = new Vector3(
                position.x,
                position.y,
                position.z);
        }
    }
}