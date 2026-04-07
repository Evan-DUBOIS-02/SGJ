using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Requests/depedencies [FR]")]
    [SerializeField] private GameObject requestsParentFR;
    [SerializeField] private GameObject depedenciesParentFR;
    
    [Header("Requests/depedencies [EN]")]
    [SerializeField] private GameObject requestsParentEN;
    [SerializeField] private GameObject depedenciesParentEN;

    [Header("Poping animation")]
    [SerializeField] private float waitingTimeBetweenElements = 0.5f;
    [SerializeField] private float waitingTimeBetweenHideShow = 0.8f;

    [Header("UI fade animation")]
    [SerializeField] private float fadeDuration;
    private bool isFadingOut = false;

    [Header("Ending text [FR]")]
    [TextArea(7, 10)]
    public string architecturalEndingDescriptionFR;
    [TextArea(7, 10)]
    public string landscapedEndingDescriptionFR;
    [TextArea(7, 10)]
    public string ecologicalEndingDescriptionFR;
    [TextArea(7, 10)]
    public string hybridEndingDescriptionFR;
    
    [Header("Ending text [EN]")]
    [TextArea(7, 10)]
    public string architecturalEndingDescriptionEN;
    [TextArea(7, 10)]
    public string landscapedEndingDescriptionEN;
    [TextArea(7, 10)]
    public string ecologicalEndingDescriptionEN;
    [TextArea(7, 10)]
    public string hybridEndingDescriptionEN;

    [Header("Ending button")]
    [SerializeField] private TMP_Text buttonReloadTextUI;
    [SerializeField] private TMP_Text buttonMenuTextUI;
    [SerializeField] private Button buttonReloadUI;
    [SerializeField] private Button buttonMenuUI;

    [Header("Menu")]
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIInGame;

    private Requests[] listRequestFR;
    private Requests[] listRequestEN;
    
    private int currentRequest = 0;

    private Depedencies[] listDepedencyFR;
    private Depedencies[] listDepedencyEN;

    private int totalArchitecturalPoints = 0;
    private int totalLandscapedPoints = 0;
    private int totalEcologicalPoints = 0;

    [SerializeField] AudioManager audioManager;
    
    private LanguageManager languageManager;
    private AchievementsManager achievementsManager;
    private void Start()
    {
        languageManager = GetComponentInParent<LanguageManager>();
        achievementsManager = GetComponent<AchievementsManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        listRequestFR = requestsParentFR.GetComponentsInChildren<Requests>();
        listRequestEN = requestsParentEN.GetComponentsInChildren<Requests>();
        listDepedencyFR = depedenciesParentFR.GetComponentsInChildren<Depedencies>();
        listDepedencyEN = depedenciesParentEN.GetComponentsInChildren<Depedencies>();

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

        // LoadRequest();
    }

    public void LoadRequest()
    {
        StartCoroutine(TextFade(0.0f, 1.0f, false));
    }

    public void PressButtonA()
    {
        // disable buttons
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        
        List<GameObject>[] tabGO = new List<GameObject>[2];
        AudioClip clipHide;
        AudioClip clipShow;
        if (languageManager.isFrench)
        {
            // getting GO hide/show / sound
            tabGO = listRequestFR[currentRequest].SetSA(1);
            clipHide = listRequestFR[currentRequest].AudioClipHideA;
            clipShow = listRequestFR[currentRequest].AudioClipShowA;
            // Adding points
            totalArchitecturalPoints += listRequestFR[currentRequest].AarchitecturalPoints;
            totalLandscapedPoints += listRequestFR[currentRequest].AlandscapedPoints;
            totalEcologicalPoints += listRequestFR[currentRequest].AecologicalPoints;
        }
        else
        {
            // getting GO hide/show / sound
            tabGO = listRequestEN[currentRequest].SetSA(1);
            clipHide = listRequestEN[currentRequest].AudioClipHideA;
            clipShow = listRequestEN[currentRequest].AudioClipShowA;
            // Adding points
            totalArchitecturalPoints += listRequestEN[currentRequest].AarchitecturalPoints;
            totalLandscapedPoints += listRequestEN[currentRequest].AlandscapedPoints;
            totalEcologicalPoints += listRequestEN[currentRequest].AecologicalPoints;
        }

        // UI fade out
        StartCoroutine(TextFade(1.0f, 0.0f, false));
        StartCoroutine(ButtonFade(1.0f, 0.0f));
        // Depedencies
        CheckDepedencies();
        // poping
        StartCoroutine(Poping(tabGO[0], tabGO[1], clipHide, clipShow, () => SwitchRequest()));
    }

    public void PressButtonB()
    {
        // disable buttons
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        List<GameObject>[] tabGO = new List<GameObject>[2];
        AudioClip clipHide;
        AudioClip clipShow;
        if (languageManager.isFrench)
        {
            // getting GO hide/show / sound
            tabGO = listRequestFR[currentRequest].SetSA(2);
            clipHide = listRequestFR[currentRequest].AudioClipHideB;
            clipShow = listRequestFR[currentRequest].AudioClipShowB;
            // Adding points
            totalArchitecturalPoints += listRequestFR[currentRequest].BarchitecturalPoints;
            totalLandscapedPoints += listRequestFR[currentRequest].BlandscapedPoints;
            totalEcologicalPoints += listRequestFR[currentRequest].BecologicalPoints;
        }
        else
        {
            // getting GO hide/show / sound
            tabGO = listRequestEN[currentRequest].SetSA(2);
            clipHide = listRequestEN[currentRequest].AudioClipHideB;
            clipShow = listRequestEN[currentRequest].AudioClipShowB;
            // Adding points
            totalArchitecturalPoints += listRequestEN[currentRequest].BarchitecturalPoints;
            totalLandscapedPoints += listRequestEN[currentRequest].BlandscapedPoints;
            totalEcologicalPoints += listRequestEN[currentRequest].BecologicalPoints;
        }

        // UI fade out
        StartCoroutine(TextFade(1.0f, 0.0f, false));
        StartCoroutine(ButtonFade(1.0f, 0.0f));
        // Depedencies
        CheckDepedencies();
        // poping
        StartCoroutine(Poping(tabGO[0], tabGO[1], clipHide, clipShow, () => SwitchRequest()));
    }

    private void CheckDepedencies()
    {
        if (languageManager.isFrench)
        {
            foreach (var dep in listDepedencyFR)
            {
                dep.CheckDepedency();
            }
        }
        else
        {
            foreach (var dep in listDepedencyEN)
            {
                dep.CheckDepedency();
            }
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
        achievementsManager.HideAllInGameIcon();
        // Reset menu button color
        buttonMenuUI.gameObject.SetActive(false);
        buttonMenuUI.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
        buttonMenuTextUI.color = new Color(1, 1, 1, 0);

        // Reset reload button
        buttonReloadUI.gameObject.SetActive(false);
        buttonReloadUI.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0);
        buttonReloadTextUI.color = new Color(1, 1, 1, 0);

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

        if (languageManager.isFrench)
        {
            // reset request answers
            foreach(Requests r in listRequestFR)
                r.ResetStatus();
            // reset depedencies status
            foreach(Depedencies d in listDepedencyFR)
                d.ResetStatus();
        }
        else
        {
            // reset request answers
            foreach(Requests r in listRequestEN)
                r.ResetStatus();
            // reset depedencies status
            foreach(Depedencies d in listDepedencyEN)
                d.ResetStatus();
        }
        
        currentRequest = 0;
        totalArchitecturalPoints = 0;
        totalLandscapedPoints = 0;
        totalEcologicalPoints = 0;
    }

    private void SwitchRequest()
    {
        // Achievements
        // StartCoroutine(achievementsManager.CheckAllAchievements());
        achievementsManager.CheckAllAchievementsIcon();

        currentRequest++;
        if (languageManager.isFrench && currentRequest >= listRequestFR.Length)
            LoadEndScene();
        else if(currentRequest >= listRequestEN.Length)
            LoadEndScene();
        else
            LoadRequest();
    }

    private void LoadEndScene()
    {
        if (totalArchitecturalPoints > totalEcologicalPoints && totalArchitecturalPoints > totalLandscapedPoints)
        {
            if(languageManager.isFrench)
                descriptionUI.text = architecturalEndingDescriptionFR;
            else
                descriptionUI.text = architecturalEndingDescriptionEN;
            // StartCoroutine(achievementsManager.setArchiUnlock());
            achievementsManager.setArchiUnlockIcon();
        }

        else if (totalLandscapedPoints > totalEcologicalPoints && totalLandscapedPoints > totalArchitecturalPoints)
        {
            if(languageManager.isFrench)
                descriptionUI.text = landscapedEndingDescriptionFR;
            else
                descriptionUI.text = landscapedEndingDescriptionEN;
            // StartCoroutine(achievementsManager.setPaysagerUnlock());
            achievementsManager.setPaysagerUnlockIcon();
        }

        else if (totalEcologicalPoints > totalLandscapedPoints && totalEcologicalPoints > totalArchitecturalPoints)
        {
            if(languageManager.isFrench)
                descriptionUI.text = ecologicalEndingDescriptionFR;
            else
                descriptionUI.text = ecologicalEndingDescriptionEN;
            // StartCoroutine(achievementsManager.setEcoloUnlock());
            achievementsManager.setEcoloUnlockIcon();
        }
        else
        {
            if(languageManager.isFrench)
                descriptionUI.text = hybridEndingDescriptionFR;
            else
                descriptionUI.text = hybridEndingDescriptionEN;
            // StartCoroutine(achievementsManager.setHybridUnlock());
            achievementsManager.setHybridUnlockIcon();
        }

        StartCoroutine(TextFade(0.0f, 1.0f, true));
    }

    private IEnumerator Poping(List<GameObject> hide, List<GameObject> show, AudioClip clipHide, AudioClip clipShow, System.Action onComplete)
    {
        if(clipHide != null)
            audioManager.PlaySFX(clipHide);

        for (int i = 0; i < hide.Count; i++)
        {
            hide[i].GetComponent<Animator>().SetTrigger("PopDown");
            yield return new WaitForSeconds(waitingTimeBetweenElements);
        }

        yield return new WaitForSeconds(waitingTimeBetweenHideShow);

        if(clipShow != null)
        {
            audioManager.SFXSource.Stop();
            audioManager.PlaySFX(clipShow);
        }

        for (int i = 0; i < show.Count; i++)
        {
            show[i].SetActive(true);
            yield return new WaitForSeconds(waitingTimeBetweenElements);
        }

        onComplete?.Invoke();
    }

    #region elements fading
    private IEnumerator TextFade(float startAlpha, float endAlpha, bool endScene)
    {
        // On fade out
        if (startAlpha == 1)
            isFadingOut = true;
        // On fade in
        else if(startAlpha == 0)
        {
            // si on est deja en fade out, on attend
            if (isFadingOut)
                while (isFadingOut)
                    yield return null;
            // On change les textes
            if(languageManager.isFrench && currentRequest < listRequestFR.Length)
            {
                descriptionUI.text = listRequestFR[currentRequest].description;
                buttonATextUI.text = listRequestFR[currentRequest].answerA;
                buttonBTextUI.text = listRequestFR[currentRequest].answerB;
            }
            else if (currentRequest < listRequestEN.Length)
            {
                descriptionUI.text = listRequestEN[currentRequest].description;
                buttonATextUI.text = listRequestEN[currentRequest].answerA;
                buttonBTextUI.text = listRequestEN[currentRequest].answerB;
            }
        }

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

        if (startAlpha == 1)
            isFadingOut = false;

        if (endAlpha == 1 && !endScene)
            StartCoroutine(ButtonFade(0, 1));
        else if(endAlpha == 1 && endScene)
            StartCoroutine(EndingButtonFade(0, 1));
    }

    private IEnumerator ButtonFade(float startAlpha, float endAlpha)
    {
        if(startAlpha == 1)
        {
            buttonAUI.GetComponent<Button>().interactable = false;
            buttonBUI.GetComponent<Button>().interactable = false;
        }
        else
        {
            buttonAUI.GetComponent<Button>().interactable = true;
            buttonBUI.GetComponent<Button>().interactable = true;
            buttonAUI.gameObject.SetActive(true);
            buttonBUI.gameObject.SetActive(true);
        }
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
        if(endAlpha == 1)
        {
            buttonAUI.enabled = true;
            buttonBUI.enabled = true;
        }
        else
        {
            buttonAUI.gameObject.SetActive(false);
            buttonBUI.gameObject.SetActive(false);
        }
    }

    private IEnumerator EndingButtonFade(float startAlpha, float endAlpha)
    {
        buttonMenuUI.enabled = false;
        buttonReloadUI.enabled = false;
        buttonMenuUI.gameObject.SetActive(true);
        buttonReloadUI.gameObject.SetActive(true);

        float elapsedTime = 0f;

        // Button Reload background color
        UnityEngine.UI.Image buttonReloadImage = buttonReloadUI.gameObject.GetComponent<UnityEngine.UI.Image>();
        Color buttonReloadColor = buttonReloadImage.color;

        // Button Reload text color
        Color buttonReloadTextColor = buttonReloadTextUI.color;

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

        buttonMenuUI.enabled = true;
        buttonReloadUI.enabled = true;
    }
    #endregion
}
