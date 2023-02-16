using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider effectsSlider;

    private void Start()
    {
        var musicsVolume = PlayerPrefs.GetFloat("VolumeOfMusics", 0);
        var effectsVolume = PlayerPrefs.GetFloat("VolumeOfEffects", 0);

        audioMixer.SetFloat("VolumeOfMusics", musicsVolume);
        audioMixer.SetFloat("VolumeOfEffects", effectsVolume);

    }

    private void OnEnable()
    {
        musicSlider.value = MathF.Pow(10,  PlayerPrefs.GetFloat("VolumeOfMusics", 0) / 20.0f);
        effectsSlider.value = MathF.Pow(10, PlayerPrefs.GetFloat("VolumeOfEffects", 0) / 20.0f);
        // SetEffectsVolume();
        // SetMusicVolume();
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("VolumeOfMusics", Mathf.Log10(musicSlider.value) * 20);
    }

    public void SetEffectsVolume()
    {
        audioMixer.SetFloat("VolumeOfEffects", Mathf.Log10(effectsSlider.value) * 20);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("VolumeOfMusics", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("VolumeOfEffects", Mathf.Log10(effectsSlider.value) * 20);
    }
}