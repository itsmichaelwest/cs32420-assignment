using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MenuController
{
    public GameObject           Menu;
    private static GameObject   MenuStatic;
    private static Button       RestartButton;

    void Start()
    {
        MenuStatic = Menu;
        MenuStatic.SetActive(false);
        RestartButton = transform.GetChild(1).GetComponent<Button>();
        RestartButton.onClick.AddListener(RestartLevel);
    }

    void Update()
    {
        if (Input.GetKey(Keys.REVERSE))
        {
            TimeController.buffersPaused = false;
            gameStarted = true;
            MenuStatic.SetActive(false);
        }
    }

    public static void GameOver()
    {
        MenuStatic.SetActive(true);
        Time.timeScale = 0;
        TimeController.buffersPaused = true;
        gameStarted = false;
    }
}
