using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public IEnumerator setArchiUnlock()
    {
        if(!ArchiUnlock)
        {
            ArchiUnlock = true;
            Debug.Log("Archi obtenu");
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière architectural";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
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

    public IEnumerator setPaysagerUnlock()
    {
        if (!paysagerUnlock)
        {
            paysagerUnlock = true;
            Debug.Log("Paysager obtenu");
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière paysager";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
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

    public IEnumerator setEcoloUnlock()
    {
        if (!ecoloUnlock)
        {
            ecoloUnlock = true;
            Debug.Log("Ecolo obtenu");
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière écologique";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
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

    public IEnumerator setHybridUnlock()
    {
        if (!hybridUnlock)
        {
            hybridUnlock = true;
            Debug.Log("Hybrid obtenu");
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière hybride";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
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

    public IEnumerator CheckAllAchievements()
    {
        TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
        Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();

        if (!corneilleUnlock && checkCorneille())
        {
            Debug.Log("Coneille obtenu");
            achievementText.text = "Succes obtenu - Corneille";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!insectesUnlock && checkInsectes())
        {
            Debug.Log("Insectes obtenu");
            achievementText.text = "Succes obtenu - Insectes";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!FleursAUnlock && checkFleursA())
        {
            Debug.Log("Fleurs A obtenu");
            achievementText.text = "Succes obtenu - Fleurs naturelles";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!FleursBUnlock && checkFleursB())
        {
            Debug.Log("Fleurs B obtenu");
            achievementText.text = "Succes obtenu - Fleurs en hommage";
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
    }

    #region UI
    [Header("General UI")]
    [SerializeField] private GameObject notObtainUI;
    [SerializeField] private GameObject achievementsPopUp;
    public bool animationPopupAvailable = true;

    public void HideAllInfo()
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
