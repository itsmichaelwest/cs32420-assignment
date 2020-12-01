using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderController : MonoBehaviour
{
    public GameObject FloatingPlatforms;

    [Range(0, .3f)] public float MovementSmoothing = .05f;
    public float MoveForce = 10f;

    /// <summary>
    /// Stop gravity impacting the player while they climb on the ladder,
    /// otherwise they'd just fall down as they tried to climb.
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            FloatingPlatforms.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }


    /// <summary>
    /// While the player is within the ladder's confines, we will allow them to
    /// climb up using vertical input.
    /// </summary>
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!(collision.tag == "Player")) return;

        float move = Input.GetAxis("Vertical");
        collision.attachedRigidbody.velocity = new Vector2(collision.attachedRigidbody.velocity.x, move * MoveForce);
    }


    /// <summary>
    /// Restores gravity to the player once they exit the ladder.
    /// </summary>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            FloatingPlatforms.gameObject.GetComponent<TilemapCollider2D>().enabled = true;
        }
    }
}