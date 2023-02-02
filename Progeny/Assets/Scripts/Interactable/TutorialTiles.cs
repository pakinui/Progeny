using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTiles : MonoBehaviour
{
    
    /**
        0 = shooting tut
        1 = climbing tut
        2 = pouncing/crouching tut
    */
    public int targetTutorial;



    //private booleans to check if tutorials have been activated
    //or is it better to just delete the obj after the tutorial is complete??
    private bool[] tutorialsCompleted = new bool[3]{false, false, false};
    


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.tag == "Player"){
            //trigger tutorial
            if(targetTutorial == 0 && !tutorialsCompleted[0]){
                ShootingTutorial();
            }else if(targetTutorial == 1 && !tutorialsCompleted[1]){
                ClimbingTutorial();
            }else if(targetTutorial == 2 && !tutorialsCompleted[2]){
                CrouchingTutorial();
            }

            //else tutorial has been done already

        }
    }







    void ShootingTutorial(){
        Debug.Log("shooting tutorial");
        Time.timeScale = 0;


        tutorialsCompleted[0] = true;
    }

    void ClimbingTutorial(){



        tutorialsCompleted[1] = true;
    }

    void CrouchingTutorial(){



        tutorialsCompleted[2] = true;
    }
}
