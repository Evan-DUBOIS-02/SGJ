using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text descriptionUI;
    [SerializeField] private TMP_Text buttonATextUI;
    [SerializeField] private TMP_Text buttonBTextUI;

    [Header("Requests")]
    [SerializeField] private GameObject requestsParent;

    [Header("Poping animation")]
    [SerializeField] private float waitingTimeBetweenElements = 0.5f;
    [SerializeField] private float waitingTimeBetweenHideShow = 0.8f;

    private Requests[] listRequest;
    private int currentRequest = 0;

    private void Start()
    {
        listRequest = requestsParent.GetComponentsInChildren<Requests>();
        LoadRequest();
    }

    private void LoadRequest()
    {
        descriptionUI.text = listRequest[currentRequest].description;
        buttonATextUI.text = listRequest[currentRequest].answerA;
        buttonBTextUI.text = listRequest[currentRequest].answerB;
    }

    public void PressButtonA()
    {
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(1);
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    public void PressButtonB() 
    {
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(2);
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    private void SwitchRequest()
    {
        Debug.Log("Tito");
    }

    private IEnumerator Poping(List<GameObject> hide, List<GameObject> show, System.Action onComplete)
    {
        for (int i = 0; i < hide.Count; i++)
        {
            hide[i].GetComponent<Animator>().SetTrigger("PopDown");
            yield return new WaitForSeconds(waitingTimeBetweenElements);
        }

        yield return new WaitForSeconds(waitingTimeBetweenHideShow);

        for (int i = 0; i < show.Count; i++)
        {
            show[i].SetActive(true);
            yield return new WaitForSeconds(waitingTimeBetweenElements);
        }

        onComplete?.Invoke();
    }
}
