using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static bool gameStarted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Keys.PAUSE))
        {
            if (PauseMenuController.MenuStatic.activeSelf != true)
                PauseMenuController.Show();
            else
                PauseMenuController.Hide();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool isGameStarted() { return gameStarted;  }
}
