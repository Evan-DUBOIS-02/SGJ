using UnityEngine;

public class CemeteryElement : MonoBehaviour
{
    public void TriggerActivation()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.one;
    }
}
