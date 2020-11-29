using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MenuController
{
    public GameObject           Menu;
    public static GameObject    MenuStatic;
    private static Button       ResumeButton;
    private static Button       RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuStatic = Menu;
        MenuStatic.SetActive(false);
        ResumeButton = transform.GetChild(1).GetComponent<Button>();
        ResumeButton.onClick.AddListener(Hide);
        RestartButton = transform.GetChild(2).GetComponent<Button>();
        RestartButton.onClick.AddListener(RestartLevel);
    }



    public static void Show()
    {
        Time.timeScale = 0;
        MenuStatic.SetActive(true);
        gameStarted = false;
    }

    public static void Hide()
    {
        MenuStatic.SetActive(false);
        Time.timeScale = 1;
        gameStarted = true;
    }
}
