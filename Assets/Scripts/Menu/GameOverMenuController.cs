using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MenuController
{
    public GameObject           Menu;
    private static GameObject   MenuStatic;
    private static Button       RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuStatic = Menu;
        MenuStatic.SetActive(false);
        RestartButton = transform.GetChild(1).GetComponent<Button>();
        RestartButton.onClick.AddListener(RestartLevel);
    }

    public static void GameOver()
    {
        MenuStatic.SetActive(true);
        Time.timeScale = 0;
        gameStarted = false;
    }
}
