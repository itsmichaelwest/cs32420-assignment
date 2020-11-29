using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static string    _NextLevel;
    public string           NextLevel;

    private void Start()
    {
        _NextLevel = NextLevel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            VictoryMenuController.Show();
        }
    }
}
