using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    
    [Header("Cemetery")]
    [SerializeField] private GameObject cemetery;

    [Header("In Game UI")]
    [SerializeField] private TMP_Text descriptionUI;
    private TMP_Text buttonATextUI;
    private TMP_Text buttonBTextUI;
    [SerializeField] private Button buttonAUI;
    [SerializeField] private Button buttonBUI;

    [Header("Requests/depedencies")]
    [SerializeField] private List<DependencyData> dependencies;
    // Avoid changing original version
    private List<DependencyData> runtimeDependencies;
    // Register all choice made by the player
    private Dictionary<RequestData, int> choiceMade = new Dictionary<RequestData, int>();

    [Header("Popping animation")]
    [SerializeField] private float waitingTimeBetweenElements = 0.5f;
    [SerializeField] private float waitingTimeBetweenHideShow = 0.8f;

    [Header("UI fade animation")]
    [SerializeField] private float fadeDuration;
    private bool isFadingOut = false;

    [Header("Ending text")]
    [TextArea(7, 10)]
    public string architecturalEndingDescriptionFR;
    [TextArea(7, 10)]
    public string landscapedEndingDescriptionFR;
    [TextArea(7, 10)]
    public string ecologicalEndingDescriptionFR;
    [TextArea(7, 10)]
    public string hybridEndingDescriptionFR;

    [Header("Ending button")]
    [SerializeField] private TMP_Text buttonReloadTextUI;
    [SerializeField] private TMP_Text buttonMenuTextUI;
    [SerializeField] private Button buttonReloadUI;
    [SerializeField] private Button buttonMenuUI;

    private int totalArchitecturalPoints = 0;
    private int totalLandscapedPoints = 0;
    private int totalEcologicalPoints = 0;
    
    private CodexManager _codexManager;
    private DialogueManager dialogueManager;
    private Dictionary<ChoiceGroup, List<ChoiceObject>> _allChoiceObjects = new Dictionary<ChoiceGroup, List<ChoiceObject>>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buttonATextUI = buttonAUI.GetComponentInChildren<TMP_Text>();
        buttonBTextUI = buttonBUI.GetComponentInChildren<TMP_Text>();
        
        runtimeDependencies = new List<DependencyData>(dependencies);
        
        // Get some manager
        _codexManager = GetComponent<CodexManager>();
        dialogueManager = GetComponent<DialogueManager>();

        // Hide UIs
        buttonReloadUI.gameObject.SetActive(false);
        buttonMenuUI.gameObject.SetActive(false);

        // By pass first spawn animations
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

    public void RegisterChoiceObject(ChoiceObject choiceObject, ChoiceGroup group)
    {
        if(!_allChoiceObjects.ContainsKey(group))
            _allChoiceObjects.Add(group, new List<ChoiceObject>());
        
        _allChoiceObjects[group].Add(choiceObject);
        _allChoiceObjects[group].Sort();
    }

    public void LoadRequest()
    {
        StartCoroutine(TextFade(0.0f, 1.0f, false));
    }

    public void PressButton(int choiceIndex)
    {
        // disable buttons
        buttonAUI.enabled = false;
        buttonBUI.enabled = false;
        
        choiceMade.Add(dialogueManager.CurrentNode.RequestData, choiceIndex);

        // Adding points
        totalArchitecturalPoints += dialogueManager.CurrentNode.Choices[choiceIndex].ChoiceData.ArchitecturalPoints;
        totalLandscapedPoints += dialogueManager.CurrentNode.Choices[choiceIndex].ChoiceData.LandscapedPoints;
        totalEcologicalPoints += dialogueManager.CurrentNode.Choices[choiceIndex].ChoiceData.EcologicalPoints;

        // UI fade out
        StartCoroutine(TextFade(1.0f, 0.0f, false));
        StartCoroutine(ButtonFade(1.0f, 0.0f));
        
        // Depedencies
        CheckDepedencies();

        // Hide and show elements
        ChoiceGroup hideGroup = dialogueManager.CurrentNode.Choices[choiceIndex].ChoiceData.HideObjectGroup;
        ChoiceGroup showGroup = dialogueManager.CurrentNode.Choices[choiceIndex].ChoiceData.ShowObjectGroup;
        List<GameObject> objectsToHide = new List<GameObject>();
        if (_allChoiceObjects.ContainsKey(hideGroup))
        {
            foreach (ChoiceObject obj in _allChoiceObjects[hideGroup])
                objectsToHide.Add(obj.gameObject);
        }
        List<GameObject> objectsToShow = new List<GameObject>();
        if (_allChoiceObjects.ContainsKey(showGroup))
        {
            foreach (ChoiceObject obj in _allChoiceObjects[showGroup])
                objectsToShow.Add(obj.gameObject);
        }
        
        // popping
        StartCoroutine(Poping(objectsToHide, objectsToShow, () => SwitchRequest()));
    }

    private void CheckDepedencies()
    {
        // For each existing dependencies
        for(int i = runtimeDependencies.Count - 1; i >= 0; i--)
        {
            DependencyData dep = runtimeDependencies[i];
            
            // If dependency met conditions
            if (dep.CheckDependency(choiceMade))
            {
                Debug.Log("New dependency !!");
                // Show all needed objects
                if (_allChoiceObjects.TryGetValue(dep.ShowObjectGroup, out var objects))
                {
                    foreach (ChoiceObject obj in objects)
                        obj.gameObject.SetActive(true);
                }
                // Do not check dependency anymore
                runtimeDependencies.RemoveAt(i);
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
        SceneManager.LoadScene("S_MainMenu");
    }

    private void ReloadData()
    {
        // _codexManager.HideAllInGameIcon();
        dialogueManager.Init();
        choiceMade.Clear();
        
        // Reset dependencies
        runtimeDependencies = new List<DependencyData>(dependencies);
        foreach (DependencyData dep in dependencies)
        {
            // Hide all needed objects
            if (_allChoiceObjects.TryGetValue(dep.ShowObjectGroup, out var objects))
            {
                foreach (ChoiceObject obj in objects)
                    obj.gameObject.SetActive(false);
            }
        }
        
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
        
        totalArchitecturalPoints = 0;
        totalLandscapedPoints = 0;
        totalEcologicalPoints = 0;
    }

    private void SwitchRequest()
    {
        // _codexManager.CheckAllAchievementsIcon();
        if(dialogueManager.CurrentNode == null)
            LoadEndScene();
        else
            LoadRequest();
    }

    private void LoadEndScene()
    {
        if (totalArchitecturalPoints > totalEcologicalPoints && totalArchitecturalPoints > totalLandscapedPoints)
        {
            descriptionUI.text = architecturalEndingDescriptionFR;
            // _codexManager.setArchiUnlockIcon();
        }

        else if (totalLandscapedPoints > totalEcologicalPoints && totalLandscapedPoints > totalArchitecturalPoints)
        {
            descriptionUI.text = landscapedEndingDescriptionFR;
            // _codexManager.setPaysagerUnlockIcon();
        }

        else if (totalEcologicalPoints > totalLandscapedPoints && totalEcologicalPoints > totalArchitecturalPoints)
        {
            descriptionUI.text = ecologicalEndingDescriptionFR;
            // _codexManager.setEcoloUnlockIcon();
        }
        else
        {
            descriptionUI.text = hybridEndingDescriptionFR;
            // _codexManager.setHybridUnlockIcon();
        }

        StartCoroutine(TextFade(0.0f, 1.0f, true));
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

            if (!endScene)
            {
                // Change description
                descriptionUI.text = dialogueManager.CurrentNode.DialogueText;
                // Change button A
                buttonATextUI.text = dialogueManager.CurrentNode.Choices[0].ChoiceData.ChoiceText;
                buttonAUI.onClick.AddListener(() =>
                {
                    PressButton(0);
                    dialogueManager.SetNextNode(dialogueManager.CurrentNode.Choices[0].DestinationNodeId);
                    buttonAUI.onClick.RemoveAllListeners();
                    buttonBUI.onClick.RemoveAllListeners();
                });
                // Change button B
                buttonBTextUI.text = dialogueManager.CurrentNode.Choices[1].ChoiceData.ChoiceText;
                buttonBUI.onClick.AddListener(() =>
                {
                    PressButton(1);
                    dialogueManager.SetNextNode(dialogueManager.CurrentNode.Choices[1].DestinationNodeId);
                    buttonAUI.onClick.RemoveAllListeners();
                    buttonBUI.onClick.RemoveAllListeners();
                });
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
