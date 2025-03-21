using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    #region corneille
    [Header("Corneille")]
    [SerializeField] private GameObject _birdWestGO;
    [SerializeField] private GameObject corneilleUIInfo;
    private bool corneilleUnlock;

    private bool checkCorneille()
    {
        if(_birdWestGO.activeSelf)
            corneilleUnlock = true;
        return corneilleUnlock;
    }

    public void ShowCorneilleInfo()
    {
        HideAllInfo();
        if (corneilleUnlock)
            corneilleUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region insectes
    [Header("Insectes")]
    [SerializeField] private GameObject _fliesVFXGO;
    [SerializeField] private GameObject insectesUIInfo;
    private bool insectesUnlock;

    private bool checkInsectes()
    {
        if (_fliesVFXGO.activeSelf)
            insectesUnlock = true;
        return insectesUnlock;
    }

    public void ShowInsectInfo()
    {
        HideAllInfo();
        if (insectesUnlock)
            insectesUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region FleursA
    [Header("FleursA")]
    [SerializeField] private GameObject _FleursAGO;
    [SerializeField] private GameObject FleursAUIInfo;
    private bool FleursAUnlock;

    private bool checkFleursA()
    {
        if (_FleursAGO.activeSelf)
            FleursAUnlock = true;
        return FleursAUnlock;
    }

    public void ShowFleursAInfo()
    {
        HideAllInfo();
        if (FleursAUnlock)
            FleursAUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region FleursB
    [Header("FleursB")]
    [SerializeField] private GameObject _FleursBGO;
    [SerializeField] private GameObject FleursBUIInfo;
    private bool FleursBUnlock;

    private bool checkFleursB()
    {
        if (_FleursBGO.activeSelf)
            FleursBUnlock = true;
        return FleursBUnlock;
    }

    public void ShowFleursBInfo()
    {
        HideAllInfo();
        if (FleursBUnlock)
            FleursBUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region archi
    [Header("Archi")]
    [SerializeField] private GameObject ArchiUIInfo;
    private bool ArchiUnlock;

    public void setArchiUnlock()
    {
        if(!ArchiUnlock)
        {
            ArchiUnlock = true;
            // + anim succe
            Debug.Log("Archi obtenu");
        }
    }

    public void ShowArchiInfo()
    {
        HideAllInfo();
        if (ArchiUnlock)
            ArchiUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region paysager
    [Header("paysager")]
    [SerializeField] private GameObject paysagerUIInfo;
    private bool paysagerUnlock;

    public void setPaysagerUnlock()
    {
        if (!paysagerUnlock)
        {
            paysagerUnlock = true;
            // + anim succe
            Debug.Log("paysager obtenu");
        }
    }

    public void ShowPaysagerInfo()
    {
        HideAllInfo();
        if (paysagerUnlock)
            paysagerUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region ecolo
    [Header("ecolo")]
    [SerializeField] private GameObject ecoloUIInfo;
    private bool ecoloUnlock;

    public void setEcoloUnlock()
    {
        if (!ecoloUnlock)
        {
            ecoloUnlock = true;
            // + anim succe
            Debug.Log("ecolo obtenu");
        }
    }

    public void ShowEcoloInfo()
    {
        HideAllInfo();
        if (ecoloUnlock)
            ecoloUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    #region hybrid
    [Header("hybrid")]
    [SerializeField] private GameObject hybridUIInfo;
    private bool hybridUnlock;

    public void setHybridUnlock()
    {
        if (!hybridUnlock)
        {
            hybridUnlock = true;
            // + anim succe
            Debug.Log("hybrid obtenu");
        }
    }

    public void ShowHybridInfo()
    {
        HideAllInfo();
        if (hybridUnlock)
            hybridUIInfo.SetActive(true);
        else
            ShowNotObtain();
    }
    #endregion

    public void CheckAllAchievements()
    {
        if (!corneilleUnlock && checkCorneille())
            // Affichage du succe in game
            Debug.Log("Corneille obtenu");
        if(!insectesUnlock && checkInsectes())
            // Affichage du succe in game
            Debug.Log("Insectes obtenu");
        if (!FleursAUnlock && checkFleursA())
            // Affichage du succe in game
            Debug.Log("Fleurs A obtenu");
        if (!FleursBUnlock && checkFleursB())
            // Affichage du succe in game
            Debug.Log("Fleurs B obtenu");
    }

    #region UI
    [Header("General UI")]
    [SerializeField] private GameObject notObtainUI;

    private void HideAllInfo()
    {
        notObtainUI.SetActive(false);
        corneilleUIInfo.SetActive(false);
        insectesUIInfo.SetActive(false);
        FleursAUIInfo.SetActive(false);
        FleursBUIInfo.SetActive(false);
        ArchiUIInfo.SetActive(false);
        paysagerUIInfo.SetActive(false);
        ecoloUIInfo.SetActive(false);
        hybridUIInfo.SetActive(false);
    }

    private void ShowNotObtain()
    {
        notObtainUI.SetActive(true);
    }
    #endregion
}
