using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour {

    public GameObject blackOutSquare;
    public bool isBlack = false;
    public bool isTransparent = true;
    public Color objectColor;

    public IEnumerator FadeBlackSquare(bool fadeToBlack = true, float fadeSpeed = 0.25f)
    {
        objectColor = blackOutSquare.GetComponent<Image>().color;
        blackOutSquare = GameObject.Find("BlackSquare");
        float fadeAmount;
        if (fadeToBlack){
            isTransparent = false;
            while (objectColor.a < 1){
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                if (blackOutSquare != null){
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                }
                yield return null;
            }
            isBlack = true;
        }
        else{
            isBlack = false;
            while (objectColor.a > 0){
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                if (blackOutSquare != null){
                    blackOutSquare.GetComponent<Image>().color = objectColor;
                }
                yield return null;
            }
            isTransparent = true;
        }
    } 
    
    public void InstantBlack(){
        isBlack = true;
        isTransparent = false;
        blackOutSquare.GetComponent<Image>().color = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
    } 
}