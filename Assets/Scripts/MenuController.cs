using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject PauseMenu;
    public Button StartButton;
    public Button ResumeButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);

        StartButton.onClick.AddListener(HideMainMenu);
        ResumeButton.onClick.AddListener(HidePausePanel);
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
}
