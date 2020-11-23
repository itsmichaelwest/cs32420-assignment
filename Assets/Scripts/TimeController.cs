using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    public GameObject player;

    public ArrayList playerPositions;
    public bool reversing = false;

    private const string KEY_REWIND = "r";

    // Start is called before the first frame update
    void Start()
    {
        playerPositions = new ArrayList();
    }

    void Update()
    {
        // If the user is pressing the reverse key, set reversing to be true.
        if (Input.GetKeyDown(KEY_REWIND))
        {
            Debug.Log("Reversing triggered!");
            reversing = true;

            Debug.Log("Reversing now!");
            player.transform.position = (Vector3)playerPositions[playerPositions.Count - 1];
            playerPositions.RemoveAt(playerPositions.Count - 1);
        }
        else
        {
            // We aren't reversing right now, so store the player position
            // Store the player's position on each update, we can then rewind the action
            //playerPositions.Add(player.transform.position);
        }
    }

    /*
    void FixedUpdate()
    {
        if (!reversing)
        {
            // We aren't reversing right now, so store the player position
            // Store the player's position on each update, we can then rewind the action
            playerPositions.Add(player.transform.position);
        }
        else
        {
            Debug.Log("Reversing now!");
            player.transform.position = (Vector3)playerPositions[playerPositions.Count - 1];
            playerPositions.RemoveAt(playerPositions.Count - 1);
        }
    }
    */
}