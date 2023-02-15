using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimator : MonoBehaviour
{
    public Sprite[] deathSprites;
    private SpriteRenderer animRenderer;
    public float framesPerSecond;
    private float timeAtAnimStart;
    

    // Start is called before the first frame update
    void Start()
    {
        animRenderer = GetComponent<Renderer>() as SpriteRenderer;
        timeAtAnimStart = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        float timeSinceAnimStart = Time.timeSinceLevelLoad - timeAtAnimStart;
        int frameIndex = (int) (timeSinceAnimStart * framesPerSecond);

        if(frameIndex < deathSprites.Length){
            animRenderer.sprite = deathSprites[frameIndex];
        }else{
            Destroy(this.gameObject);
        }
    }
}
