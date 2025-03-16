using UnityEngine;

public class CemeteryElement : MonoBehaviour
{
    public bool startStatus;
    public GameObject vfx;
    private Vector3 baseScale;
    private Vector3 basePosition;
    public AudioClip audioClip;

    private void Start()
    {
        baseScale = transform.localScale;
        basePosition = transform.localPosition;
    }

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void TriggerActivation()
    {
        gameObject.SetActive(false);
        transform.localScale = baseScale;
        transform.localPosition = basePosition;
    }

    public void ResetStatus()
    {
        gameObject.SetActive(startStatus);
        if (startStatus)
        {
            GetComponent<Animator>().SetTrigger("ByPassAnim");
        }
    }

    public void SpawnVFX()
    {
        if (vfx != null)
        {
            Vector3 pos = transform.position;
            pos.y = -1f;
            Destroy(Instantiate(vfx, pos, Quaternion.Euler(90, 0, 0)), 5f);
        }
    }

    public void PlayEnvSound()
    {
        audioManager.PlaySFX(audioClip);
    }
}
