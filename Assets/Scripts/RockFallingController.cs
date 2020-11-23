using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallingController : MonoBehaviour
{
    Rigidbody2D rb2D;
    PlayerCharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.bodyType = RigidbodyType2D.Static;

        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    /// <summary>
    /// Use the physics engine to run raycast operations. If the player stands
    /// below the object, restore physics to it.
    ///
    /// TODO: This needs to be updated to instanciate rocks falling from a
    /// source vs a single object falling.
    /// </summary>
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider.gameObject.tag == "Player")
        {
            // TODO: Update this
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            player.SetHealth(-100);
        }

        Debug.DrawRay(transform.position, -Vector2.up, Color.red);
    }
}