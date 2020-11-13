using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesController : MonoBehaviour
{
    public PlayerCharacterController player;
    private CircleCollider2D collider2D;
    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = player.cc;
        tilemap = GetComponent<Tilemap>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            player.SetHealth(-100);
        }
    }
}