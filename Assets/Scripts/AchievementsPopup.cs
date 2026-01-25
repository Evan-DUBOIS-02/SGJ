using UnityEngine;

public class AchievementsPopup : MonoBehaviour
{
    [SerializeField] AchievementsManager manager;
    public void TriggerEndAnimation()
    {
        manager.animationPopupAvailable = true;
    }
}
