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
            }
        }
    }

    public void ResetStatus()
    {
        isEventTrigger = false;
    }
}
