using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void OnEnable()
    {
        audioMixer.SetFloat("VolumeOfMusic", PlayerPrefs.GetFloat("VolumeOfMusic", 0.75f));
        audioMixer.SetFloat("VolumeOfEffects", PlayerPrefs.GetFloat("VolumeOfEffects", 0.75f));
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("VolumeOfMusic", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetEffectsVolume()
    {
        audioMixer.SetFloat("VolumeOfEffects", Mathf.Log10(effectsSlider.value) * 20);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("VolumeOfMusic", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("VolumeOfEffects", Mathf.Log10(effectsSlider.value) * 20);
    }
}