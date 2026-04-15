using System;
using UnityEngine;

public enum FinalChoice
{
    Home,
    Neutral,
    Manif
}

public class DataSaver: MonoBehaviour
{
    public FinalChoice finalChoice = FinalChoice.Neutral;
    public int totalPublicPoints;
    public int totalPrivatePoints;
    public int totalPublicActions;
    public int totalPrivateActions;
    
    public static DataSaver instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
