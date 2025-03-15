using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIInGame;
    [SerializeField] private GameObject UICredits;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    public void StartGame()
    {
        UIMainMenu.SetActive(false);
        UIInGame.SetActive(true);
        gameManager.LoadRequest();
    }

    public void ShowCredits()
    {
        UIMainMenu.SetActive(false);
        UICredits.SetActive(true);
    }

    public void BackCreditsButton()
    {
        UICredits.SetActive(false);
        UIMainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
