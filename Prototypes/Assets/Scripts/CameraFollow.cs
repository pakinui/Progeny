using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : it can be replaced by cinemachine: https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/index.html


public class CameraFollow : MonoBehaviour
{
    public float yOffset = 3;
    public float xOffset = 3;
    public Transform target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
    }
}
