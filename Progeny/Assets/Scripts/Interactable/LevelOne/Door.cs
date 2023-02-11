using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject displayPrefab;
    private GameObject display;
    private bool isUsable = false;

    private BoxCollider2D boxCol;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsable && Input.GetKeyDown("e")){
            _animator.SetTrigger("OpenDoor");
            Destroy(display);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            isUsable = true;
            display = Instantiate(displayPrefab, this.transform.parent);
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"){
            isUsable = false;
            Destroy(display);
        }
    }

    public void StopDoor(){
        _animator.enabled = false;
        boxCol.enabled = false;

    }

}
