using UnityEngine;

public class CemeteryController : MonoBehaviour
{
    [SerializeField] private Animator cemeteryAnimator;

    private bool isPlaying = true;
    private int currentDirection = 1;
        
    public void RotateLeft()
    {
        currentDirection = -1;
        cemeteryAnimator.SetFloat("RotationDirection", currentDirection);
        if(!isPlaying)
            PlayPause();
    }

    public void PlayPause()
    {
        isPlaying = !isPlaying;
        cemeteryAnimator.speed = isPlaying ? 1 : 0;
    }

    public void RotateRight()
    {
        currentDirection = 1;
        cemeteryAnimator.SetFloat("RotationDirection", currentDirection);
        if(!isPlaying)
            PlayPause();
    }
}
