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
                volumeValue = AudioManagerNEW.instance.MasterVolume;
                break;
            case MixerGroup.Music:  
                volumeValue = AudioManagerNEW.instance.MusicVolume;
                break;
            case MixerGroup.SFX:
                volumeValue = AudioManagerNEW.instance.SFXVolume;
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
                AudioManagerNEW.instance.SetMasterVolume(volume);
                break;
            case MixerGroup.Music:
                AudioManagerNEW.instance.SetMusicVolume(volume);
                break;
            case MixerGroup.SFX:
                AudioManagerNEW.instance.SetSFXVolume(volume);
                break;
        }
    }
}
