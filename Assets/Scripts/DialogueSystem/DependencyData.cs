using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ChoiceCondition
{
    public RequestData requestData;
    public int choice;
}

[CreateAssetMenu(fileName = "RequestData", menuName = "Dialogue System/DependencyData")]
public class DependencyData: ScriptableObject
{
    public List<ChoiceCondition> conditions;
    public ChoiceGroup ShowObjectGroup;
    public CodexEntryData codexEntry;

    public bool CheckDependency(Dictionary<RequestData, int> choiceMade)
    {
        foreach (ChoiceCondition condition in conditions)
        {
            if (choiceMade.ContainsKey(condition.requestData) == false) 
                return false;
            if (choiceMade[condition.requestData] != condition.choice)
                return false;
        }
        
        CodexManager.Instance.UnlockEntry(codexEntry);
        return true;
    }
}
