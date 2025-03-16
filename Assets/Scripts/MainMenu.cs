using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIInGame;
    [SerializeField] private GameObject UICredits;

    private GameManager gameManager;
    private AudioManager audioManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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
}
