using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider Slider1;
    public Slider musicSlider;
    public Slider Slider3;

    private void OnEnable()
    {
        audioMixer.SetFloat("VolumeOfMusic", PlayerPrefs.GetFloat("VolumeOfMusic", 0.75f));
    }
    public void SetMasterVolume()
    {
        //audioMixer.SetFloat( "VolumeOfMusic", Slider1.value );
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("VolumeOfMusic", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetMusic2Volume()
    {
        //audioMixer.SetFloat( "Music2Param", Slider3.value );
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("VolumeOfMusic", Mathf.Log10(musicSlider.value) * 20);
    }
}