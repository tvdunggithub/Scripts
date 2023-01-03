using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        //do we have saved Volume in playerprefs???
        if(PlayerPrefs.HasKey("EffectVolume"))
        {
            //set the mixer volume levels based on the saved playerprefs
            mixer.SetFloat("EffectVolume", PlayerPrefs.GetFloat("EffectVolume"));
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));

            SetSliders();
        }
        else
        {
            //if we didnt save the value just set a default value
            SetSliders();
        }
    }

    //called at the start of the game
    //set the slider value to be the saved volume setting
    void SetSliders()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateSFXVolume()
    {
        //Update Value and save it to player prefs
        mixer.SetFloat("EffectVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("EffectVolume", sfxSlider.value);
    }

    public void UpdateMusicVolume()
    {
        //Update Value and save it to player prefs
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
