using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum MixerGroup
{
    Master,
    Music,
    SFX
}

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Source ----")]
    public AudioSource musicSource;
    public AudioSource ambianceSource;

    [Header("---- Audio Mixer Group ----")] 
    [SerializeField]
    private AudioMixer _audioMixer;
    [SerializeField] public AudioMixerGroup musicGroup;
    [SerializeField] public AudioMixerGroup sfxGroup;

    private float masterVolume;
    public float MasterVolume { get { return masterVolume; } }
    private float musicVolume;
    public float  MusicVolume { get { return musicVolume; } }
    private float sfxVolume;
    public float SFXVolume { get { return sfxVolume; } }
    
    [Header("---- Audio Coroutine ----")]
    public float fadeDuration = 1f;
    Coroutine musicCoroutine;
    private Coroutine ambianceCoroutine;
    
    private float startTimeMusic;
    private bool shouldLoop;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetMasterVolume(0.5f);
        SetMusicVolume(0.5f);
        SetSFXVolume(0.5f);
    }

    public void SetMusicClip(AudioClip musicClip, int musicS = 0, float startTime = 0f, bool shouldLoop = true)
    { 
        startTimeMusic = startTime;
        this.shouldLoop = shouldLoop;
        PlayMusic(musicClip, musicS); 
    }
    
    public void PlayMusic(AudioClip newClip, int musicS)
    {
        if (musicS == 0)
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.outputAudioMixerGroup = musicGroup;
            }
        
            if (musicSource.clip == newClip)
                return;

            if (musicCoroutine != null)
                StopCoroutine(musicCoroutine);

            musicCoroutine = StartCoroutine(FadeAndSwitch(newClip));
        }
        else if (musicS == 1)
        {
            if (ambianceSource == null)
            {
                ambianceSource = gameObject.AddComponent<AudioSource>();
                ambianceSource.outputAudioMixerGroup = musicGroup;
            }
        
            if (ambianceSource.clip == newClip)
                return;

            if (ambianceCoroutine != null)
                StopCoroutine(ambianceCoroutine);

            ambianceCoroutine = StartCoroutine(FadeAndSwitchAmbiance(newClip));
        }
    }

    IEnumerator FadeAndSwitch(AudioClip newClip)
    {
        // Fade out
        float startVolume = musicSource.volume;
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.time = startTimeMusic;
        musicSource.loop = shouldLoop;
        musicSource.Play();

        // Fade in
        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.volume = startVolume;
    }
    
    IEnumerator FadeAndSwitchAmbiance(AudioClip newClip)
    {
        // Fade out
        float startVolume = ambianceSource.volume;
        while (ambianceSource.volume > 0)
        {
            ambianceSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        ambianceSource.Stop();
        ambianceSource.clip = newClip;
        ambianceSource.time = startTimeMusic;
        ambianceSource.loop = shouldLoop;
        ambianceSource.Play();

        // Fade in
        while (ambianceSource.volume < startVolume)
        {
            ambianceSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        ambianceSource.volume = startVolume;
    }

    public void PlaySFXClip(AudioClip clip)
    {
        StartCoroutine(PlayAndDestroySFXClip(clip));
    }
    
    public IEnumerator PlayAndDestroySFXClip(AudioClip clip)
    {
        GameObject go = new GameObject("TempAudio");
        go.transform.parent = transform;

        AudioSource source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = sfxGroup;
        source.clip = clip;
        source.Play();

        yield return new WaitWhile(() => source.isPlaying);

        Destroy(go);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    public void PauseSound()
    {
        musicSource.Pause();
    }

    public void ResumeSound()
    {
        musicSource.UnPause();
    }
}
