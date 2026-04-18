using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDialogueGraph : ScriptableObject
{
    public string EntryNodeId;
    public List<RuntimeDialogueNode> AllNodes = new List<RuntimeDialogueNode>();
}

[Serializable]
public class RuntimeDialogueNode
{
    public string NodeId;
    public string DialogueText;
    public List<ChoiceNodeData> Choices = new List<ChoiceNodeData>();
    public string NextNodeId;
    public RequestData RequestData;
}

[Serializable]
public class ChoiceNodeData
{
    public ChoiceData ChoiceData;
    public string DestinationNodeId;
}
