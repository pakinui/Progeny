using Unity.VisualScripting;
using UnityEngine;

namespace Utility
{
    public class TriggerCheck2D : MonoBehaviour
    {
        public LayerMask mLayerMask;
        public int triggerCount;

        private GameMaster gm;

        // private PlayerCrouch crouch;

        void Start(){
            gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        }

        // void OnCollisionEnter2D(Collision2D other){
        //     if(other.gameObject.name == "Tilemap"){
        //         crouch.setCanStand(false);
        //     }
        // }

        // void OnCollisionExit2D(Collision2D other){
        //     Debug.Log("something");
        //     if(other.gameObject.name == "Tilemap"){
        //         crouch.setCanStand(true);
        //     } else if(other.gameObject.tag == "Enemy"){
        //         Debug.Log("dodge");
        //     }
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CheckLayerMask(other.GameObject()))
            {
                triggerCount++;
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (CheckLayerMask(other.GameObject()))
            {
                triggerCount--;
            }
            else if (other.tag == "Enemy")
            {
                gm.slowMo(3f);
            }
        }

        private bool CheckLayerMask(GameObject other)
        {
            return mLayerMask == (mLayerMask | (1 << other.layer));
        }
    
        public bool Triggered()
        {
            return triggerCount > 0;
        }
    }

}