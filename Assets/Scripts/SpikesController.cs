using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesController : MonoBehaviour
{
    public PlayerCharacterController player;
    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.SetHealth(-100);
        }
    }
}