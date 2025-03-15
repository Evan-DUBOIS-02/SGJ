using UnityEngine;

public class CemeteryElement : MonoBehaviour
{
    public bool startStatus;

    public void TriggerActivation()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }

    public void ResetStatus()
    {
        gameObject.SetActive(startStatus);
    }
}
