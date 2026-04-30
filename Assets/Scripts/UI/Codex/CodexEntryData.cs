using UnityEngine;

[CreateAssetMenu(fileName = "CodexEntryData", menuName = "Codex/CodexEntryData")]
public class CodexEntryData: ScriptableObject
{
    public Sprite icon;
    public string title;
    [TextArea(7, 20)]
    public string description;
}
