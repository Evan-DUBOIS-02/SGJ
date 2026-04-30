using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CodexData", menuName = "Codex/CodexData")]
public class CodexData: ScriptableObject
{
    public List<CodexEntryData> entries;
}
