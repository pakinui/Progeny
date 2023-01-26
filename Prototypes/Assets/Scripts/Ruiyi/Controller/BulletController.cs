using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    private void Awake()
    {
    }

    void Start()
    {
        // TODO : send message when bullet destroyed
        Destroy(gameObject, 5);
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
    }

    private void OnDestroy()
    {
        Debug.Log("Bullet Destroyed");
    }
}
