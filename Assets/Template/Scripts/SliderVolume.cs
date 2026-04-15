using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private MixerGroup mixerGroup;

    private void OnEnable()
    {
        float volumeValue;
        switch (mixerGroup)
        {
            case MixerGroup.Master:
                volumeValue = AudioManager.instance.MasterVolume;
                break;
            case MixerGroup.Music:  
                volumeValue = AudioManager.instance.MusicVolume;
                break;
            case MixerGroup.SFX:
                volumeValue = AudioManager.instance.SFXVolume;
                break;
            
            default:
                volumeValue = 1;
                break;
        }
        gameObject.GetComponent<Slider>().value = volumeValue;
    }

    public void SetVolume()
    {
        float volume = gameObject.GetComponent<Slider>().value;
        switch (mixerGroup)
        {
            case MixerGroup.Master:
                AudioManager.instance.SetMasterVolume(volume);
                break;
            case MixerGroup.Music:
                AudioManager.instance.SetMusicVolume(volume);
                break;
            case MixerGroup.SFX:
                AudioManager.instance.SetSFXVolume(volume);
                break;
        }
    }
}
