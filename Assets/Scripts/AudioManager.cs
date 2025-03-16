using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] AudioSource AMBSourceWood1;
    [SerializeField] AudioSource AMBSourceWood2;
    [SerializeField] AudioSource AMBSourceGrass1;
    [SerializeField] AudioSource AMBSourceGrass2;
    [SerializeField] AudioSource AMBSourceRuche1;
    [SerializeField] AudioSource AMBSourceRuche2;

    [Header("------- Music -------")]
    public AudioClip MUSBackground;

    [Header("------- Audio Clip Ambiance -------")]
    public AudioClip AMBTree1;
    public AudioClip AMBTree2;
    public AudioClip AMBGrass1;
    public AudioClip AMBGrass2;
    public AudioClip AMBRuche1;
    public AudioClip AMBRuche2;

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

    [Header("------- Table of Audio -------")]
    public AudioClip[] tabAudioClick;
    public AudioClip[] tabAudioMvt;

    private int idLastAudioClick = -1;
    private int idLastAudioMvt = -1;

    private void Start()
    {
        StartAmbiance();
    }

    public void StartAmbiance()
    {
        //RESTART
        musicSource.Stop();
        SFXSource.Stop();
        AMBSourceGrass1.Stop();
        AMBSourceGrass2.Stop();
        AMBSourceWood1.Stop();
        AMBSourceWood2.Stop();
        AMBSourceRuche1.Stop();
        AMBSourceRuche2.Stop();

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
    }

    public void GenerateSound(int typeOfSound) //1 = Ambiance , 2 = Click , 3 = Movement
    {
        if(typeOfSound == 2)
        {
            int soundRandom = Random.Range(0, tabAudioClick.Length);

            while(soundRandom == idLastAudioClick)
                soundRandom = Random.Range(0, tabAudioClick.Length);

            idLastAudioClick = soundRandom;
            PlaySFX(tabAudioClick[soundRandom]);
        }

        else if(typeOfSound == 3)
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

    public void ManageAmbiance(int requestId, int idAnswer) //idAnswer => 1 = A, 2 = B
    {
        switch (requestId)
        {
            case 1:
                if(idAnswer == 2)
                {
                    AMBSourceGrass1.Play();
                    AMBSourceGrass2.Play();
                }
                break;

            case 2:
                if(idAnswer == 1)
                {
                    AMBSourceWood1.Stop();
                    AMBSourceWood2.Stop();
                }
                break;

            case 3:
                if(idAnswer == 1)
                {
                    AMBSourceGrass1.Stop();
                    AMBSourceGrass2.Stop();
                }
                break;

            case 5:
                if(idAnswer == 2)
                {
                    AMBSourceRuche1.Play();
                    AMBSourceRuche2.Play();
                }
                break;

            default:
                break;
        }
    }
}
