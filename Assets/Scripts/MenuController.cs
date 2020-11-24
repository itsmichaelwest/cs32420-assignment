using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject           MainMenu;
    public GameObject           PauseMenu;
    public GameObject           GameOverMenu;
    public Button               StartButton;
    public Button               ResumeButton;
    public Button               ResetButton;

    public bool                 isGameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);

        StartButton.onClick.AddListener(HideMainMenu);
        ResumeButton.onClick.AddListener(HidePausePanel);

        ResetButton.onClick.AddListener(RestartLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.activeSelf != true)
            {
                ShowPausePanel();
            }
            else
            {
                HidePausePanel();
            }
        }
    }


    private void RestartLevel()
    {
        SceneManager.LoadScene("Level1");
    }


    public void HideMainMenu()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        isGameStarted = true;
    }

    public void ShowPausePanel()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
        isGameStarted = false;
    }

    private void HidePausePanel()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        isGameStarted = true;
    }

    public void ShowGameOverMenu()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0;
        isGameStarted = false;
    }
}
