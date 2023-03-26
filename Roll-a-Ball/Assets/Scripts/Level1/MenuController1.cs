using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController1 : MonoBehaviour
{
    public Button pauseButton;
    public GameObject pausePanel;
    public Button resumeButton;
    public Button menuButton;
    public Button restartButton;
    public PlayerController1 playerController1;
    public Button level1Button, level2Button;
    public Button exitButton;

    void Start()
    {
        playerController1 = GameObject.FindObjectOfType<PlayerController1>();
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
          playerController1.RestartGame();
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