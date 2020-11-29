using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUpdater : MonoBehaviour
{
    private PlayerCharacterController   player;
    private Text                        healthMeter;

    // Start is called before the first frame update
    void Start()
    {
        healthMeter = GetComponent<Text>();
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        healthMeter.text = "Health: " + player.ReturnHealth();
    }
}