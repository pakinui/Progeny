using System;
using Interface;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        Rigidbody2D mRigidbody2D;
        
        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void Update()
        {
            
        }

        public void SetForce(Vector2 force, ForceMode2D forceMode)
        {
            mRigidbody2D.AddForce(force, forceMode);
        }
        
        public Vector2 Velocity
        {
            get => mRigidbody2D.velocity;
            set => mRigidbody2D.velocity = value;
        }

        public Vector2 Position => mRigidbody2D.position;

        public float Rotation => mRigidbody2D.rotation;

        public void TryAim()
        {
            throw new NotImplementedException();
        }

        public void TryAttack()
        {
            throw new NotImplementedException();
        }
    }
}