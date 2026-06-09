using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;


[Serializable]
[CreateAssetMenu(fileName = "RequestData", menuName = "Dialogue System/RequestData")]
public class RequestData: ScriptableObject
{
    [Header("Description")]
    [SerializeField] public LocalizedString localizedDescription;
    [TextArea(7, 20)]
    public string Description;
    public List<ChoiceData> Choices;
}

[Serializable]
public struct ChoiceData
{
    public string ChoiceText;
    public ChoiceGroup HideObjectGroup;
    public ChoiceGroup ShowObjectGroup;
    public int ArchitecturalPoints;
    public int LandscapedPoints;
    public int EcologicalPoints;
}
