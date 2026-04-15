using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public RuntimeDialogueGraph RuntimeGraph;

    [Header("UI Components")] 
    public GameObject DialoguePanel;
    public TMP_Text SpeakerNameText;
    public TMP_Text DialogueText;
    
    [Header("Choice Button UI")]
    public Button ChoiceButtonPrefab;
    public Transform ChoiceButtonContainer;
    
    private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new Dictionary<string, RuntimeDialogueNode>();
    private RuntimeDialogueNode _currentNode;

    private void Start()
    {
        foreach (var node in RuntimeGraph.AllNodes)
        {
            _nodeLookup[node.NodeId] = node;
        }

        if (!string.IsNullOrEmpty(RuntimeGraph.EntryNodeId))
        {
            ShowNode(RuntimeGraph.EntryNodeId);
        }
        else
        {
            EndDialogue();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentNode != null && _currentNode.Choices.Count == 0)
        {
            if (!string.IsNullOrEmpty(_currentNode.NextNodeId))
            {
                ShowNode(_currentNode.NextNodeId);
            }
            else
            {
                EndDialogue();
            }
        }
    }

    private void ShowNode(string nodeId)
    {
        if (!_nodeLookup.ContainsKey(nodeId))
        {
            EndDialogue();
            return;
        }
        
        _currentNode = _nodeLookup[nodeId];
        
        DialoguePanel.SetActive(true);
        SpeakerNameText.SetText(_currentNode.SpeakerName);
        DialogueText.SetText(_currentNode.DialogueText);

        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }

        if (_currentNode.Choices.Count > 0)
        {
            foreach (var choice in _currentNode.Choices)
            {
                Button button = Instantiate(ChoiceButtonPrefab, ChoiceButtonContainer);
                TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
                if (buttonText != null)
                {
                    buttonText.SetText(choice.ChoiceText);
                }

                if (button != null)
                {
                    button.onClick.AddListener(() =>
                    {
                        if (!string.IsNullOrEmpty(choice.DestinationNodeId))
                        {
                            ShowNode(choice.DestinationNodeId);
                        }
                        else
                        {
                            EndDialogue();
                        }
                    });
                }
            }
        }
    }

    private void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        _currentNode = null;
        
        foreach (Transform child in ChoiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
