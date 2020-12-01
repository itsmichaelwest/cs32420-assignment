using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUpdater : MonoBehaviour
{
    private PlayerCharacterController   player;
    private Text                        healthMeter;

    void Start()
    {
        healthMeter = GetComponent<Text>();
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    void Update()
    {
        healthMeter.text = "Health: " + player.ReturnHealth();
    }
}