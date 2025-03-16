using UnityEngine;

public class CemeteryElement : MonoBehaviour
{
    public bool startStatus;
    public GameObject vfx;
    private Vector3 baseScale;
    private Vector3 basePosition;

    private void Start()
    {
        baseScale = transform.localScale;
        basePosition = transform.localPosition;
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
            pos.y = 0.4f;
            Destroy(Instantiate(vfx, pos, Quaternion.Euler(90, 0, 0)), 5f);
        }
    }
}
