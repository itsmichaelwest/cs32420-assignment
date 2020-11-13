using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    [Range(0, .3f)] public float MovementSmoothing = .05f;
    public float MoveForce = 10f;

    /// <summary>
    /// Stop gravity impacting the player while they climb on the ladder, otherwise they'd
    /// just fall down as they tried to climb.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("collide");
            collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }



    void OnTriggerStay2D(Collider2D collider)
    {
        if (!(collider.tag == "Player")) { return; }

        Debug.Log("colliding");

        float move = Input.GetAxis("Vertical");
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetVelocity = new Vector2(collider.attachedRigidbody.velocity.y, move * MoveForce);
        collider.attachedRigidbody.velocity = Vector3.SmoothDamp(collider.attachedRigidbody.velocity, targetVelocity, ref currentVelocity, MovementSmoothing);


        //collider.transform.Translate(new Vector3(0, y, 0));
    }



    /// <summary>
    /// Restores gravity to the player once they exit the ladder.
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Debug.Log("no collide");
            collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}