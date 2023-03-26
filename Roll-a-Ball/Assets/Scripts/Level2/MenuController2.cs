using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController2 : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pausePanel;
    public Button resumeButton;
    public Button menuButton;
    public Button restartButton;
    public PlayerController2 playerController2;
    public Button exitButton;

    private void Awake()
    {
        // Cauta referinta la playerController2 inainte de a initializa butoanele
        playerController2 = FindObjectOfType<PlayerController2>();
    }

    void Start()
    {
        // Initializarea butoanelor
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        pausePanel = GameObject.Find("PausePanel");
        resumeButton = pausePanel.transform.Find("ResumeButton").GetComponent<Button>();
        menuButton = pausePanel.transform.Find("MenuButton").GetComponent<Button>();
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(GoToMainMenu);
        restartButton.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        // Daca playerController2 nu a fost gasit, il cautam din nou
        if (playerController2 == null)
        {
            playerController2 = FindObjectOfType<PlayerController2>();
        }

        // Verificam daca am gasit referinta si apelam metoda RestartGame
        if (playerController2 != null)
        {
            playerController2.RestartGame();
        }
    }

    public void OnLevel1ButtonPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGame");
    }

    public void OnLevel2ButtonPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");

    }

    public void PauseGame()
    {
        if (!pausePanel.activeSelf)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}