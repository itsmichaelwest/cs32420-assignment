﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallingController : MonoBehaviour
{
    public GameObject       rock;

    private const int       MAX_ROCK_SPAWNS = 10;
    private int             rockCount = 0;          // Always starts at 0.

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Use the physics engine to run raycast operations.
    /// </summary>
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if ((hit.collider.gameObject.tag == "Player") && (rockCount <= MAX_ROCK_SPAWNS))
        {
            GameObject child = Instantiate(rock, transform.position, Quaternion.identity);
            child.transform.parent = transform;
            rockCount++;
        }
    }

    public void DecreaseRockCount(int count)
    {
        rockCount = rockCount - count;
    }
}