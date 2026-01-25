using DefaultNamespace;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIInGame;
    [SerializeField] private GameObject UICredits;

    private GameManager gameManager;
    private AudioManager audioManager;
    private TextReferencer  textReferencer;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        textReferencer = GetComponent<TextReferencer>();
    }
    public void StartGame()
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UIInGame.SetActive(true);
        gameManager.LoadRequest();
    }

    public void ShowCredits()
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UICredits.SetActive(true);
    }

    public void BackCreditsButton()
    {
        audioManager.GenerateSound(2);
        UICredits.SetActive(false);
        UIMainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.GenerateSound(2);
        Application.Quit();
    }

    public void SetToFrenchMode()
    {
        gameManager.isFrench = true;
        gameManager.isEnglish = false;
        textReferencer.SwitchAllTextToFrench();
    }

    public void SetToEnglishMode()
    {
        gameManager.isFrench = false;
        gameManager.isEnglish = true;
        textReferencer.SwitchAllTextToEnglish();
    }
}
