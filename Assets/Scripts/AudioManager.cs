using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioSource AMBSourceWood1;
    [SerializeField] public AudioSource AMBSourceWood2;
    [SerializeField] public AudioSource AMBSourceGrass1;
    [SerializeField] public AudioSource AMBSourceGrass2;
    [SerializeField] public AudioSource AMBSourceRuche1;
    [SerializeField] public AudioSource AMBSourceRuche2;
    [SerializeField] public AudioSource AMBSourceCorneille;

    [Header("------- Music -------")]
    public AudioClip MUSBackground;

    [Header("------- Audio Clip Ambiance -------")]
    public AudioClip AMBTree1;
    public AudioClip AMBTree2;
    public AudioClip AMBGrass1;
    public AudioClip AMBGrass2;
    public AudioClip AMBRuche1;
    public AudioClip AMBRuche2;
    public AudioClip AMBCorneille;
    public AudioClip AMBFly;

    /*
    [Header("------- Audio Clip UI -------")]
    public AudioClip SFXClicButton1;
    public AudioClip SFXClicButton2;
    public AudioClip SFXClicButton3;

    public AudioClip SFXMovButton1;
    public AudioClip SFXMovButton2;
    public AudioClip SFXMovButton3;
    public AudioClip SFXMovButton4;

    */

    /*
    [Header("SOUND GENERATOR")]

    [SerializeField] private float minTimeBetweenSounds = 2.0f;
    [SerializeField] private float maxTimeBetweenSounds = 10.0f;

    [SerializeField] private float timeBetweenSound;
    */

    [Header("------- Table of Audio -------")]
    public AudioClip[] tabAudioClick;
    public AudioClip[] tabAudioMvt;

    private int idLastAudioClick = -1;
    private int idLastAudioMvt = -1;
    public float timeToFadeIn = 1f;
    public float timeToFadeOut = 3f;
    [SerializeField] private float volumeFadeInAmbiance = 0.8f;

    private void Start()
    {
        musicSource.clip = MUSBackground;
        musicSource.Play();

        AMBSourceWood1.clip = AMBTree1;
        AMBSourceWood2.clip = AMBTree2;
        AMBSourceWood1.Play();
        AMBSourceWood2.Play();

        AMBSourceGrass1.clip = AMBGrass1;
        AMBSourceGrass2.clip = AMBGrass2;

        AMBSourceRuche1.clip = AMBRuche1;
        AMBSourceRuche2.clip = AMBRuche2;
        //StartAmbiance();

        AMBSourceCorneille.clip = AMBCorneille;
    }

    public void StartAmbiance()
    {
        //RESTART
        /*
        SFXSource.Stop();
        AMBSourceGrass1.Stop();
        AMBSourceGrass2.Stop();
        AMBSourceWood1.Stop();
        AMBSourceWood2.Stop();
        AMBSourceRuche1.Stop();
        AMBSourceRuche2.Stop();
        */
        if (AMBSourceGrass1.isPlaying)
        {
            StartCoroutine(FadeAmbiance(false, AMBSourceGrass1, timeToFadeOut, 0f));
            AMBSourceGrass1.Stop();
        }
        if (AMBSourceGrass2.isPlaying)
        {
            StartCoroutine(FadeAmbiance(false, AMBSourceGrass2, timeToFadeOut, 0f));
            AMBSourceGrass2.Stop();
        }
        if (AMBSourceRuche1.isPlaying)
        {
            StartCoroutine(FadeAmbiance(false, AMBSourceRuche1, timeToFadeOut, 0f));
            AMBSourceRuche1.Stop();
        }
        if (AMBSourceRuche2.isPlaying)
        {
            StartCoroutine(FadeAmbiance(false, AMBSourceRuche2, timeToFadeOut, 0f));
            AMBSourceRuche2.Stop();
        }

        if (AMBSourceCorneille.isPlaying)
        {
            StartCoroutine(FadeAmbiance(false, AMBSourceCorneille, timeToFadeOut, 0f));
            AMBSourceCorneille.Stop();
        }

        if (!AMBSourceWood1.isPlaying)
        {
            StartCoroutine(FadeAmbiance(true, AMBSourceWood1, timeToFadeIn, volumeFadeInAmbiance));
        }
        if (!AMBSourceWood2.isPlaying)
        {
            StartCoroutine(FadeAmbiance(true, AMBSourceWood2, timeToFadeIn, 0.5f));
        }
        //timeBetweenSound = GenerateRandomTimeBtwSound();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        timeBetweenSound -= Time.deltaTime;
        if (timeBetweenSound <= 0)
        {
            GenerateSound(1);
            timeBetweenSound = GenerateRandomTimeBtwSound();
        }
    }

    public float GenerateRandomTimeBtwSound()
    {
        return Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }
    */

    public void GenerateSound(int typeOfSound) //1 = Ambiance , 2 = Click , 3 = Movement
    {
        if (typeOfSound == 2)
        {
            int soundRandom = Random.Range(0, tabAudioClick.Length);

            while (soundRandom == idLastAudioClick)
                soundRandom = Random.Range(0, tabAudioClick.Length);

            idLastAudioClick = soundRandom;
            PlaySFX(tabAudioClick[soundRandom]);
        }

        else if (typeOfSound == 3)
        {
            int soundRandom = Random.Range(0, tabAudioMvt.Length);

            while (soundRandom == idLastAudioMvt)
                idLastAudioMvt = soundRandom;

            PlaySFX(tabAudioMvt[soundRandom]);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public IEnumerator FadeAmbiance(bool fadeIn, AudioSource audioSource, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthOfSource = (double)audioSource.clip.samples / audioSource.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }

        float time = 0f;
        float startVol = audioSource.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }

        yield break;
    }

    public void ManageAmbiance(int requestId, int idAnswer) //idAnswer => 1 = A, 2 = B
    {
        switch (requestId)
        {
            case 1:
                if (idAnswer == 2)
                {
                    AMBSourceGrass1.Play();
                    AMBSourceGrass2.Play();
                    StartCoroutine(FadeAmbiance(true, AMBSourceGrass1, timeToFadeIn, volumeFadeInAmbiance));
                    StartCoroutine(FadeAmbiance(true, AMBSourceGrass2, timeToFadeIn, volumeFadeInAmbiance));
                }
                break;

            case 2:
                if (idAnswer == 1)
                {
                    StartCoroutine(FadeAmbiance(false, AMBSourceWood1, timeToFadeOut, 0f));
                    AMBSourceWood1.Stop();
                }
                break;

            case 3:
                if (idAnswer == 1)
                {
                    StartCoroutine(FadeAmbiance(false, AMBSourceGrass1, timeToFadeOut, 0f));
                    StartCoroutine(FadeAmbiance(false, AMBSourceGrass2, timeToFadeOut, 0f));
                    AMBSourceGrass1.Stop();
                    AMBSourceGrass2.Stop();
                }
                break;

            case 5:
                if (idAnswer == 2)
                {
                    AMBSourceRuche1.Play();
                    if(!AMBSourceRuche2.isPlaying)
                        AMBSourceRuche2.Play();
                    StartCoroutine(FadeAmbiance(true, AMBSourceRuche1, timeToFadeIn, volumeFadeInAmbiance));
                    StartCoroutine(FadeAmbiance(true, AMBSourceRuche2, timeToFadeIn, volumeFadeInAmbiance));
                }
                break;

            default:
                break;
        }
    }

    public void ActivateSoundEvent(int id)
    {
        if (id == 0)
        {
            AMBSourceCorneille.Play();
            StartCoroutine(FadeAmbiance(true, AMBSourceCorneille, timeToFadeIn, volumeFadeInAmbiance));
        }
        else if (id == 1)
        {
            AMBSourceRuche2.Play();
            StartCoroutine(FadeAmbiance(true, AMBSourceRuche2, timeToFadeIn, volumeFadeInAmbiance));
        }
    }
}
