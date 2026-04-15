using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RequestData", menuName = "Dialogue System/RequestData")]
public class RequestData: ScriptableObject
{
    [Header("Description")]
    [TextArea(7, 20)]
    public string Description;
    public List<ChoiceData> Choices;
}

[Serializable]
public struct ChoiceData
{
    public string ChoiceText;
    public List<ChoiceGroup> HideObjectGroup;
    public List<ChoiceGroup> ShowObjectGroup;
    public int ArchitecturalPoints;
    public int LandscapedPoints;
    public int EcologicalPoints;
}
