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
    [Header("Cemetery")]
    [SerializeField] private GameObject cemetery;

    [Header("In Game UI")]
    [SerializeField] private TMP_Text descriptionUI;
    [SerializeField] private TMP_Text buttonATextUI;
    [SerializeField] private TMP_Text buttonBTextUI;
    [SerializeField] private Button buttonAUI;
    [SerializeField] private Button buttonBUI;

    [Header("Requests/depedencies")]
    [SerializeField] private GameObject requestsParent;
    [SerializeField] private GameObject depedenciesParent;

    [Header("Poping animation")]
    [SerializeField] private float waitingTimeBetweenElements = 0.5f;
    [SerializeField] private float waitingTimeBetweenHideShow = 0.8f;

    [Header("UI fade animation")]
    [SerializeField] private float fadeDuration;

    [Header("Ending text")]
    [TextArea(7, 10)]
    public string architecturalEndingDescription;
    [TextArea(7, 10)]
    public string landscapedEndingDescription;
    [TextArea(7, 10)]
    public string ecologicalEndingDescription;
    [TextArea(7, 10)]
    public string hybridEndingDescription;

    [Header("Ending button")]
    [SerializeField] private TMP_Text buttonReloadTextUI;
    [SerializeField] private TMP_Text buttonMenuTextUI;
    [SerializeField] private Button buttonReloadUI;
    [SerializeField] private Button buttonMenuUI;

    [Header("Menu")]
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIInGame;

    private Requests[] listRequest;
    private int currentRequest = 0;

    private Depedencies[] listDepedency;

    private int totalArchitecturalPoints = 0;
    private int totalLandscapedPoints = 0;
    private int totalEcologicalPoints = 0;

    [SerializeField] AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        listRequest = requestsParent.GetComponentsInChildren<Requests>();
        listDepedency = depedenciesParent.GetComponentsInChildren<Depedencies>();

        buttonMenuUI.gameObject.SetActive(false);
        buttonReloadUI.gameObject.SetActive(false);

        foreach(Transform t in cemetery.transform)
        {
            if(!t.gameObject.activeSelf) 
                continue;

            Animator elem;
            if (t.gameObject.TryGetComponent<Animator>(out elem))
            {
                elem.SetTrigger("ByPassAnim");
            }
            else
            {
                foreach (Transform t2 in t.transform)
                    if(!t2.gameObject.activeSelf)
                        continue;
                    else if (t2.gameObject.TryGetComponent<Animator>(out elem))
                        elem.SetTrigger("ByPassAnim");
            }
        }

        LoadRequest();
    }

    public void LoadRequest()
    {
        descriptionUI.text = listRequest[currentRequest].description;
        buttonATextUI.text = listRequest[currentRequest].answerA;
        buttonBTextUI.text = listRequest[currentRequest].answerB;
        StartCoroutine(TextFade(0.0f, 1.0f));
        StartCoroutine(ButtonFade(0.0f, 1.0f));
        buttonAUI.enabled = true;
        buttonBUI.enabled = true;
    }

    public void PressButtonA()
    {
        // disable buttons
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        // getting GO hide/show
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(1);
        // Adding points
        totalArchitecturalPoints += listRequest[currentRequest].AarchitecturalPoints;
        totalLandscapedPoints += listRequest[currentRequest].AlandscapedPoints;
        totalEcologicalPoints += listRequest[currentRequest].AecologicalPoints;
        // UI fade out
        StartCoroutine(TextFade(1.0f, 0.0f));
        StartCoroutine(ButtonFade(1.0f, 0.0f));
        // Depedencies
        CheckDepedencies();
        // poping
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    public void PressButtonB()
    {
        // disable buttons
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        // getting GO hide/show
        List<GameObject>[] tabGO = new List<GameObject>[2];
        tabGO = listRequest[currentRequest].SetSA(2);
        // Adding points
        totalArchitecturalPoints += listRequest[currentRequest].BarchitecturalPoints;
        totalLandscapedPoints += listRequest[currentRequest].BlandscapedPoints;
        totalEcologicalPoints += listRequest[currentRequest].BecologicalPoints;
        // UI fade out
        StartCoroutine(TextFade(1.0f, 0.0f));
        StartCoroutine(ButtonFade(1.0f, 0.0f));
        // Depedencies
        CheckDepedencies();
        // poping
        StartCoroutine(Poping(tabGO[0], tabGO[1], () => SwitchRequest()));
    }

    private void CheckDepedencies()
    {
        foreach(var dep in listDepedency)
        {
            dep.CheckDepedency();
        }
    }

    public void PressReloadButton()
    {
        ReloadData();
        LoadRequest();
    }

    public void PressMenuButton()
    {
        ReloadData();
        UIInGame.SetActive(false);
        UIMainMenu.SetActive(true);
    }

    private void ReloadData()
    {
        buttonMenuUI.gameObject.SetActive(false);
        buttonReloadUI.gameObject.SetActive(false);
        // reset all cemetery elements
        foreach (Transform t in cemetery.transform)
        {
            CemeteryElement elem;
            if (t.gameObject.TryGetComponent<CemeteryElement>(out elem))
            {
                elem.ResetStatus();
            }
            else
            {
                foreach (Transform t2 in t.transform)
                    if (t2.gameObject.TryGetComponent<CemeteryElement>(out elem))
                        elem.ResetStatus();
            }
        }
        // reset request answers
        foreach(Requests r in listRequest)
            r.ResetStatus();
        // reset depedencies status
        foreach(Depedencies d in listDepedency)
            d.ResetStatus();
        currentRequest = 0;
        totalArchitecturalPoints = 0;
        totalLandscapedPoints = 0;
        totalEcologicalPoints = 0;
    }

    private void SwitchRequest()
    {
        currentRequest++;
        if (currentRequest >= listRequest.Length)
            LoadEndScene();
        else
            LoadRequest();
    }

    private void LoadEndScene()
    {
        if (totalArchitecturalPoints > totalEcologicalPoints && totalArchitecturalPoints > totalLandscapedPoints)
            descriptionUI.text = architecturalEndingDescription;
        else if (totalLandscapedPoints > totalEcologicalPoints && totalLandscapedPoints > totalArchitecturalPoints)
            descriptionUI.text = landscapedEndingDescription;
        else if (totalEcologicalPoints > totalLandscapedPoints && totalEcologicalPoints > totalArchitecturalPoints)
            descriptionUI.text = ecologicalEndingDescription;
        else
            descriptionUI.text = hybridEndingDescription;

        StartCoroutine(TextFade(0.0f, 1.0f));
        StartCoroutine(EndingButtonFade(0.0f, 1.0f));
        buttonMenuUI.gameObject.SetActive(true);
        buttonReloadUI.gameObject.SetActive(true);
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

    #region elements fading
    private IEnumerator TextFade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Description text color
        Color descriptionColor = descriptionUI.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            descriptionUI.color = new Color(descriptionColor.r, descriptionColor.g, descriptionColor.b, newAlpha);
            yield return null;
        }

        descriptionUI.color = new Color(descriptionColor.r, descriptionColor.g, descriptionColor.b, endAlpha);
    }

    private IEnumerator ButtonFade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

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
            buttonAImage.color = new Color(buttonAColor.r, buttonAColor.g, buttonAColor.b, newAlpha);
            buttonATextUI.color = new Color(buttonATextColor.r, buttonATextColor.g, buttonATextColor.b, newAlpha);
            buttonBImage.color = new Color(buttonBColor.r, buttonBColor.g, buttonBColor.b, newAlpha);
            buttonBTextUI.color = new Color(buttonBTextColor.r, buttonBTextColor.g, buttonBTextColor.b, newAlpha);
            yield return null;
        }

        buttonAImage.color = new Color(buttonAColor.r, buttonAColor.g, buttonAColor.b, endAlpha);
        buttonBImage.color = new Color(buttonBColor.r, buttonBColor.g, buttonBColor.b, endAlpha);
        buttonATextUI.color = new Color(buttonATextColor.r, buttonATextColor.g, buttonATextColor.b, endAlpha);
        buttonBTextUI.color = new Color(buttonBTextColor.r, buttonBTextColor.g, buttonBTextColor.b, endAlpha);
    }

    private IEnumerator EndingButtonFade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // Button Reload background color
        UnityEngine.UI.Image buttonReloadImage = buttonReloadUI.gameObject.GetComponent<UnityEngine.UI.Image>();
        Color buttonReloadColor = buttonReloadImage.color;

        // Button Reload text color
        Color buttonReloadTextColor = buttonATextUI.color;

        // Button B background color
        UnityEngine.UI.Image buttonMenuImage = buttonMenuUI.gameObject.GetComponent<UnityEngine.UI.Image>();
        Color buttonMenuColor = buttonMenuImage.color;

        // Button B text color
        Color buttonMenuTextColor = buttonMenuTextUI.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            buttonReloadImage.color = new Color(buttonReloadColor.r, buttonReloadColor.g, buttonReloadColor.b, newAlpha);
            buttonReloadTextUI.color = new Color(buttonReloadTextColor.r, buttonReloadTextColor.g, buttonReloadTextColor.b, newAlpha);
            buttonMenuImage.color = new Color(buttonMenuColor.r, buttonMenuColor.g, buttonMenuColor.b, newAlpha);
            buttonMenuTextUI.color = new Color(buttonMenuTextColor.r, buttonMenuTextColor.g, buttonMenuTextColor.b, newAlpha);
            yield return null;
        }

        buttonReloadImage.color = new Color(buttonReloadColor.r, buttonReloadColor.g, buttonReloadColor.b, endAlpha);
        buttonMenuImage.color = new Color(buttonMenuColor.r, buttonMenuColor.g, buttonMenuColor.b, endAlpha);
        buttonReloadTextUI.color = new Color(buttonReloadTextColor.r, buttonReloadTextColor.g, buttonReloadTextColor.b, endAlpha);
        buttonMenuTextUI.color = new Color(buttonMenuTextColor.r, buttonMenuTextColor.g, buttonMenuTextColor.b, endAlpha);
    }
    #endregion
}
