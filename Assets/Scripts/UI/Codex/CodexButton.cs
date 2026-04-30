using UnityEngine;
using UnityEngine.UI;

public class CodexButton: MonoBehaviour
{
    [SerializeField] private Image image;
    private CodexEntryData codexEntryData;
    public void SetCodexEntry(CodexEntryData data, bool isUnlocked)
    {
        codexEntryData = data;
        if (isUnlocked) image.sprite = codexEntryData.icon;
        else image.sprite = null;
    }
}
