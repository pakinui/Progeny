using UnityEngine;
using System.Collections;

public class ElectricAnimation : MonoBehaviour {

   // Reference to animator component
   Animator anim;
   
   // Probability of electric
   float electricProb = 0.005f;
   
   // Use this for initialization
   void Start () {
      // Initialise the reference to the Animator component
      anim = GetComponent<Animator>();
   }

   // At fixed time intervals...
   void FixedUpdate () {
      
      // Draw a random value between 0 and 1, if it's less
      // than probability of electric starting, set the Start trigger
      // of the Animator
      float randomSample = Random.Range(0f, 1f);
      if(randomSample < electricProb) {
         anim.SetTrigger("start");
      }   
   }
}