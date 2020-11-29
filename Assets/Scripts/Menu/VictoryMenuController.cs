using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryMenuController : MenuController
{
    //public string               NextLevel;
    public GameObject           Menu;
    private static GameObject   MenuStatic;
    private static Button       NextLevelButton;
    private static Button       RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        MenuStatic = Menu;
        MenuStatic.SetActive(false);
        NextLevelButton = transform.GetChild(1).GetComponent<Button>();
        NextLevelButton.onClick.AddListener(LoadNextLevel);
        RestartButton = transform.GetChild(2).GetComponent<Button>();
        RestartButton.onClick.AddListener(RestartLevel);
    }

    public static void Show()
    {
        Time.timeScale = 0;
        MenuStatic.SetActive(true);
        gameStarted = false;
    }


    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(VictoryManager._NextLevel);
    }
}
