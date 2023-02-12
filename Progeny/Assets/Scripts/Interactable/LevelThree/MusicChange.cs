using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    public AudioClip music;
    private AudioSource cameraAudioSource;
    private CameraController cameraController;

    private bool waitForMute = false;

    void Start(){
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        cameraAudioSource = camera.GetComponent<AudioSource>();
        cameraController = camera.GetComponent<CameraController>();
    }
    void Update(){
        if (waitForMute){
            if (cameraAudioSource.volume <= 0){
                waitForMute = false;
                cameraAudioSource.clip = music;
                cameraAudioSource.pitch = 0.84f;
                cameraController.Mute(false, 0);
                cameraAudioSource.Play();
            }
        }
    }
    public void ChangeMusic(){
        cameraController.Mute(true, 4f);
        waitForMute = true;
    }
}
        