using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UICredits;
    [SerializeField] private GameObject UIAchievements;
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

    public void ShowCredits()
    {
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UICredits.SetActive(true);
    }

    public void ShowAchievements()
    {
        cemetery.SetActive(false);
        audioManager.GenerateSound(2);
        UIMainMenu.SetActive(false);
        UIAchievements.SetActive(true);
    }

    public void BackCreditsButton()
    {
        audioManager.GenerateSound(2);
        UICredits.SetActive(false);
        UIMainMenu.SetActive(true);
    }

    public void BackAchievementsButton()
    {
        audioManager.GenerateSound(2);
        UIAchievements.SetActive(false);
        UIMainMenu.SetActive(true);
        cemetery.SetActive(true);
        GetComponent<AchievementsManager>().HideAllInfo();
        foreach (Transform t in cemetery.transform)
        {
            if (!t.gameObject.activeSelf)
                continue;

            Animator elem;
            if (t.gameObject.TryGetComponent<Animator>(out elem))
            {
                elem.SetTrigger("ByPassAnim");
            }
            else
            {
                foreach (Transform t2 in t.transform)
                    if (!t2.gameObject.activeSelf)
                        continue;
                    else if (t2.gameObject.TryGetComponent<Animator>(out elem))
                        elem.SetTrigger("ByPassAnim");
            }
        }
    }

    public void QuitGame()
    {
        audioManager.GenerateSound(2);
        Application.Quit();
    }
}
