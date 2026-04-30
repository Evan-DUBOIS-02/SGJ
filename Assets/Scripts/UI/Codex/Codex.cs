using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Codex: MonoBehaviour
{
    [SerializeField] private GameObject codexButton;
    [SerializeField] private GameObject codexButtonContainer;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    
    [SerializeField] private string notUnlockedTitle;
    [SerializeField] private string notUnlockedDescription;

    private void OnEnable()
    {
        LoadEntries();
    }

    public void LoadEntries()
    {
        foreach (Transform child in codexButtonContainer.transform)
        {
            Destroy(child.gameObject);
        }
        
        if (CodexManager.Instance == null) 
            return;
        
        
        Dictionary<CodexEntryData, bool> entriesStates = CodexManager.Instance.GetEntriesStates();
        foreach (KeyValuePair<CodexEntryData, bool> entry in entriesStates)
        {
            GameObject codexButtonGo = Instantiate(codexButton, codexButtonContainer.transform);
            codexButtonGo.GetComponent<CodexButton>().SetCodexEntry(entry.Key, entry.Value);
            if (entry.Value)
            {
                codexButtonGo.GetComponent<Button>().onClick.AddListener(() =>
                {
                    titleText.text = entry.Key.title;
                    descriptionText.text = entry.Key.description;
                });
            }
            else
            {
                codexButtonGo.GetComponent<Button>().onClick.AddListener(() =>
                {
                    titleText.text = notUnlockedTitle;
                    descriptionText.text = notUnlockedDescription;
                });
            }
        }
    }
}
