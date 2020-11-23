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

    // Start is called before the first frame update
    void Start()
    {
        thing = GetComponent<Transform>();
        positions = new LinkedList<Vector3>();
    }

    void Update()
    {
        if (Input.GetKey(KEY_REWIND))
        {
            reversing = true;
            Time.timeScale = 2;
        }
        else
        {
            reversing = false;
            Time.timeScale = 1;
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
            thing.position = (Vector3)positions.Last();
            positions.RemoveLast();
        }
    }
}