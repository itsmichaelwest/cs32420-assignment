﻿using UnityEngine;
using UnityEngine.UI;

public class WelcomeMenuController : MenuController
{
    public GameObject   Menu;
    private Button      StartButton;

    void Start()
    {
        Menu.SetActive(true);
        StartButton = transform.GetChild(1).GetComponent<Button>();
        StartButton.onClick.AddListener(HideMainMenu);
    }

    public void HideMainMenu()
    {
        Time.timeScale = 1;
        Menu.SetActive(false);
        gameStarted = true;
    }
}
