using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // following code is taken from a tutorial video by Brackeys: https://www.youtube.com/watch?v=YOaYQrN1oYQ
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.75f);
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundSlider.value) * 20);
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSoundVolume()
    {
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundSlider.value) * 20);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }
}
