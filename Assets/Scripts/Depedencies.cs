using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Depedencies : MonoBehaviour
{
    [SerializeField] private Requests requestA;
    [SerializeField] private SelectedAnswer selectedAnswerA;

    [SerializeField] private Requests requestB;
    [SerializeField] private SelectedAnswer selectedAnswerB;

    [SerializeField] private List<GameObject> elementToShow = new List<GameObject>();

    private bool isEventTrigger = false;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void CheckDepedency()
    {
        if (isEventTrigger) 
            return;
        if (requestA.sa == selectedAnswerA && requestB.sa == selectedAnswerB)
        {
            isEventTrigger = true;
            foreach (var elem in elementToShow)
            {
                elem.gameObject.SetActive(true);
                if(CompareTag("Birds"))
                {
                    audioManager.ActivateSoundEvent(0);
                }
                else if(CompareTag("Fly"))
                {
                    audioManager.ActivateSoundEvent(1);
                }
            }
        }
    }

    public void ResetStatus()
    {
        isEventTrigger = false;
        foreach(var elem in elementToShow)
            elem.GetComponent<CemeteryElement>().ResetStatus();
    }
}
