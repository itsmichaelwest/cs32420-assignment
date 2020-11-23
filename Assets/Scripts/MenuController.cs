using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public Button StartButton;
    public Button ResumeButton;
    public Button ResetButton;
    public Button RewindButton;

    private PlayerCharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;

        Time.timeScale = 0;
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);

        StartButton.onClick.AddListener(HideMainMenu);
        ResumeButton.onClick.AddListener(HidePausePanel);

        ResetButton.onClick.AddListener(RestartLevel);
        RewindButton.onClick.AddListener(RewindLevel);
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

    private void RewindLevel()
    {
        player.gameObject.GetComponent<TimeController>().Rewind(20);
    }


    public void HideMainMenu()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
    }

    public void ShowPausePanel()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    private void HidePausePanel()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ShowGameOverMenu()
    {
        GameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
