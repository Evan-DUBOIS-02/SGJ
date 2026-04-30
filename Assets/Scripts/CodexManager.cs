using System.Collections.Generic;
using UnityEngine;

public class CodexManager : MonoBehaviour
{
    public static CodexManager Instance { get; set; }
    
    [SerializeField] private CodexData _codexData;
    private Dictionary<CodexEntryData, bool> _unlockEntries;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        DontDestroyOnLoad(gameObject);

        _unlockEntries = new Dictionary<CodexEntryData, bool>();
        
        // TODO: check save instead of init as not unlocked
        foreach (CodexEntryData entry in _codexData.entries)
        {
            _unlockEntries.Add(entry, false);
        }
    }

    public void UnlockEntry(CodexEntryData entry)
    {
        if (_unlockEntries.ContainsKey(entry))
        {
            _unlockEntries[entry] = true;
        }
    }

    public Dictionary<CodexEntryData, bool> GetEntriesStates()
    {
        return _unlockEntries;
    }
}
