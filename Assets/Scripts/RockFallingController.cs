using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallingController : MonoBehaviour
{
    public float floatHeight;
    public float liftForce;
    public float damping;

    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        Debug.Log("Hitting: " + hit.collider);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            float force = liftForce * heightError - rb2D.velocity.y * damping;

            Debug.Log("Applying force: " + force);

            rb2D.AddForce(Vector3.up * force);
        }
    }
}