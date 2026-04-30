using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UICredits;
    [SerializeField] private GameObject UICodex;
    [SerializeField] private GameObject UILevelSelection;
    [SerializeField] private GameObject cemetery;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    public void StartGame()
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        SceneManager.LoadScene("S_CH01");
    }

    public void StartChapter(string sceneName)
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
        
    public void ShowLevelSelection()
    {
        cemetery.SetActive(false);
        audioManager.GenerateSound(2);
        UILevelSelection.SetActive(true);
        UIMainMenu.SetActive(false);
    }

    public void ShowCredits()
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UICredits.SetActive(true);
    }

    public void ShowCodex()
    {
        cemetery.SetActive(false);
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UICodex.SetActive(true);
    }

    public void BackCreditsButton()
    {
        audioManager.GenerateSound(2);
        UICredits.SetActive(false);
        UIMainMenu.SetActive(true);
    }

    public void BackCodexButton()
    {
        audioManager.GenerateSound(2);
        UICodex.SetActive(false);
        UIMainMenu.SetActive(true);
        cemetery.SetActive(true);
    }

    public void BackLevelSelection()
    {
        audioManager.GenerateSound(2);
        UILevelSelection.SetActive(false);
        UIMainMenu.SetActive(true);
        cemetery.SetActive(true);
    }

    public void QuitGame()
    {
        audioManager.GenerateSound(2);
        Application.Quit();
    }
}
