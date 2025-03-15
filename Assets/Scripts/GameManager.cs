using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text descriptionUI;
    [SerializeField] private TMP_Text buttonATextUI;
    [SerializeField] private TMP_Text buttonBTextUI;
    [SerializeField] private Button buttonAUI;
    [SerializeField] private Button buttonBUI;

    [Header("Requests")]
    [SerializeField] private GameObject requestsParent;

    [Header("Poping animation")]
    [SerializeField] private float waitingTimeBetweenElements = 0.5f;
    [SerializeField] private float waitingTimeBetweenHideShow = 0.8f;

    [Header("UI fade animation")]
    [SerializeField] private float fadeDuration;

    private Requests[] listRequest;
    private int currentRequest = 0;

    private void Start()
    {
        listRequest = requestsParent.GetComponentsInChildren<Requests>();
        LoadRequest();
    }

    public void LoadRequest()
    {
        descriptionUI.text = listRequest[currentRequest].description;
        buttonATextUI.text = listRequest[currentRequest].answerA;
        buttonBTextUI.text = listRequest[currentRequest].answerB;
        StartCoroutine(Fade(0.0f, 1.0f));
        buttonAUI.enabled = true;
        buttonBUI.enabled = true;
    }

    public void PressButtonA()
    {
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(1);
        StartCoroutine(Fade(1.0f, 0.0f));
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    public void PressButtonB() 
    {
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(2);
        StartCoroutine(Fade(1.0f, 0.0f));
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    private void SwitchRequest()
    {
        currentRequest++;
        if (currentRequest >= listRequest.Length)
            Debug.Log("End game");
        else
            LoadRequest();
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

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Description text color
        Color descriptionColor = descriptionUI.color;

        // Button A background color
        UnityEngine.UI.Image buttonAImage = buttonAUI.gameObject.GetComponent<UnityEngine.UI.Image>();
        Color buttonAColor = buttonAImage.color;

        // Button A text color
        Color buttonATextColor = buttonATextUI.color;

        // Button B background color
        UnityEngine.UI.Image buttonBImage = buttonBUI.gameObject.GetComponent<UnityEngine.UI.Image>();
        Color buttonBColor = buttonBImage.color;

        // Button B text color
        Color buttonBTextColor = buttonBTextUI.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            descriptionUI.color = new Color(descriptionColor.r, descriptionColor.g, descriptionColor.b, newAlpha);
            buttonAImage.color = new Color(buttonAColor.r, buttonAColor.g, buttonAColor.b, newAlpha);
            buttonATextUI.color = new Color(buttonATextColor.r, buttonATextColor.g, buttonATextColor.b, newAlpha);
            buttonBImage.color = new Color(buttonBColor.r, buttonBColor.g, buttonBColor.b, newAlpha);
            buttonBTextUI.color = new Color(buttonBTextColor.r, buttonBTextColor.g, buttonBTextColor.b, newAlpha);
            yield return null;
        }

        descriptionUI.color = new Color(descriptionColor.r, descriptionColor.g, descriptionColor.b, endAlpha);
        buttonAImage.color = new Color(buttonAColor.r, buttonAColor.g, buttonAColor.b, endAlpha);
        buttonBImage.color = new Color(buttonBColor.r, buttonBColor.g, buttonBColor.b, endAlpha);
        buttonATextUI.color = new Color(buttonATextColor.r, buttonATextColor.g, buttonATextColor.b, endAlpha);
        buttonBTextUI.color = new Color(buttonBTextColor.r, buttonBTextColor.g, buttonBTextColor.b, endAlpha);
    }
}
