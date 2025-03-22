using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    #region corneille
    [Header("Corneille")]
    [SerializeField] private GameObject _birdWestGO;
    [SerializeField] private GameObject corneilleUIInfo;
    [SerializeField] private Sprite corneilleSprite;
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
    [SerializeField] private Sprite insectesSprite;
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
    [SerializeField] private Sprite fleursASprite;
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
    [SerializeField] private Sprite fleursBSprite;
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
    [SerializeField] private Sprite archiSprite;
    private bool ArchiUnlock;

    public IEnumerator setArchiUnlock()
    {
        if(!ArchiUnlock)
        {
            ArchiUnlock = true;
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière architectural";
            icon.sprite = archiSprite;
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
    [SerializeField] private Sprite paysagerSprite;
    private bool paysagerUnlock;

    public IEnumerator setPaysagerUnlock()
    {
        if (!paysagerUnlock)
        {
            paysagerUnlock = true;
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière paysager";
            icon.sprite = paysagerSprite;
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
    [SerializeField] private Sprite ecoloSprite;
    private bool ecoloUnlock;

    public IEnumerator setEcoloUnlock()
    {
        if (!ecoloUnlock)
        {
            ecoloUnlock = true;
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière écologique";
            icon.sprite = ecoloSprite;
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
    [SerializeField] private Sprite hybridSprite;
    private bool hybridUnlock;

    public IEnumerator setHybridUnlock()
    {
        if (!hybridUnlock)
        {
            hybridUnlock = true;
            TMP_Text achievementText = achievementsPopUp.GetComponentInChildren<TMP_Text>();
            Animator achievementAnimator = achievementsPopUp.GetComponentInChildren<Animator>();
            while (!animationPopupAvailable)
                yield return null;
            achievementText.text = "Succes obtenu - Cimetière hybride";
            icon.sprite = hybridSprite;
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
            achievementText.text = "Succes obtenu - Corneille";
            icon.sprite = corneilleSprite;
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!insectesUnlock && checkInsectes())
        {
            achievementText.text = "Succes obtenu - Insectes & hérisson";
            icon.sprite = insectesSprite;
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!FleursAUnlock && checkFleursA())
        {
            achievementText.text = "Succes obtenu - Fleurs naturelles";
            icon.sprite = fleursASprite;
            achievementAnimator.SetTrigger("Popup");
            animationPopupAvailable = false;
            while (!animationPopupAvailable)
                yield return null;
        }
        if (!FleursBUnlock && checkFleursB())
        {
            achievementText.text = "Succes obtenu - Fleurs en hommage";
            icon.sprite = fleursBSprite;
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
    [SerializeField] private Image icon;
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
