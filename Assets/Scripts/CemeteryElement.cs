using UnityEngine;

public class CemeteryElement : MonoBehaviour
{
    public bool startStatus;
    public GameObject vfx;

    public void TriggerActivation()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }

    public void ResetStatus()
    {
        gameObject.SetActive(startStatus);
    }

    public void SpawnVFX()
    {
        if (vfx != null)
        {
            Vector3 pos = transform.position;
            pos.y = 0.5f;
            Destroy(Instantiate(vfx, pos, Quaternion.Euler(90, 0, 0)), 5f);
        }
    }
}
