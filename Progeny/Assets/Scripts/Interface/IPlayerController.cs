using UnityEngine;

namespace Interface
{
    public interface IPlayerController
    {
        void TryAim();
        
        /*
            This is a method that will be called by the player
            when the player is in the aiming state.
        */
        void TryAttack();
    }
}