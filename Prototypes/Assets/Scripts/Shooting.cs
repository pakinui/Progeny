using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletSpawnPoint;

    private bool canFire = true;
    private float timeSinceLastShot;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); 
        // 
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotZ);

        if(!canFire){
            timeSinceLastShot += Time.deltaTime;
            if(timeSinceLastShot > cooldown){
                canFire = true;
            }
        }
        if(Input.GetMouseButton(0) && canFire){
            canFire = false;
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            timeSinceLastShot = 0;
        }   
    }
}
