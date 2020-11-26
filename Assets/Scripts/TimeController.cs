using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private Transform                   thing;
    public bool                         reversing = false;
    private LinkedList<Vector3>         positions;
    private const string                KEY_REWIND = "r";               // Key bound to reverse function
    private const int                   MAXIMUM_REVERSE_SECS = 20;      // Maximum time allowed for reversal in seconds
    private MenuController              menu;
    private PlayerCharacterController   player;

    // Start is called before the first frame update
    void Start()
    {
        thing = GetComponent<Transform>();
        positions = new LinkedList<Vector3>();
        menu = FindObjectOfType(typeof(MenuController)) as MenuController;
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    void Update()
    {
        // If user is holding the rewind key, begin reversing time. Otherwise
        // stop reversing time.
        if (Input.GetKey(KEY_REWIND))
        {
            reversing = true;
            Time.timeScale = 2;
            player.SetIsKillable(false);
        }
        else
        {
            // This is to prevent timeScale being set to 1 before the main menu
            // is dismissed.
            if (menu.isGameStarted)
            {
                reversing = false;
                Time.timeScale = 1;
                player.SetIsKillable(true);
            }
        }

        // Clear the list of positions once the rewind key is let go, as we only
        // store one rewind session.
        if (Input.GetKeyUp(KEY_REWIND))
        {
            if (menu.isGameStarted)
            {
                positions.Clear();
                player.SetIsKillable(true);
            }
        }
    }

    void FixedUpdate()
    {
        if (!reversing)
        {
            if (positions.Count() >= (MAXIMUM_REVERSE_SECS * 60))
                positions.RemoveFirst();
            positions.AddLast(thing.position);
        }
        else
        {
            if (positions.Count() != 0)
            {
                thing.position = positions.Last();
                positions.RemoveLast();
            }
        }
    }


    /// <summary>
    /// Rewind the level a specified number of seconds.
    /// </summary>
    /// <param name="seconds">The number of seconds to rewind. This
    /// cannot be higher than the maximum number of rewind seconds allowed.</param>
    public void Rewind(int seconds)
    {
        int secsToRewind;
        if (seconds <= MAXIMUM_REVERSE_SECS)
            secsToRewind = seconds;
        else
            secsToRewind = MAXIMUM_REVERSE_SECS;

        for (int i = 0; i >= MAXIMUM_REVERSE_SECS; i++)
        {
            thing.position = positions.Last();
            positions.RemoveLast();
        }
    }
}