using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUpdateManager : MonoBehaviour
{

    public PlayerCharacterController player;

    private Text healthMeter;

    // Start is called before the first frame update
    void Start()
    {
        healthMeter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        healthMeter.text = "Health: " + player.health;
    }
}