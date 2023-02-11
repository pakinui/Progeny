using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour {

    public GameObject blackOutSquare;
    public bool isBlack = false;
    public Color objectColor;


    void Update(){
        
        objectColor = blackOutSquare.GetComponent<Image>().color;
        if (!isBlack && objectColor.a >= 1){
            isBlack = true;
        }
        if (isBlack && objectColor.a <= 0){
            isBlack = false;
        }
    }

    public IEnumerator FadeBlackSquare(bool fadeToBlack = true, float fadeSpeed = 0.25f)
    {
        objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;
        if (fadeToBlack){
            while (objectColor.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else{
            while (objectColor.a > 0){
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    } 

    public void InstantBlack(){
        isBlack = true;
        blackOutSquare.GetComponent<Image>().color = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
    } 
}