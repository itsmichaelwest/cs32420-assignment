using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCharacterController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public CircleCollider2D cc;
    public bool facingRight = false;

    // Define any keys we will use, 
    private const string KEY_JUMP = "space";

    private TimeController timeController;

    // Health
    // Do not use numbers when referring to the maximum or minimum health values, instead
    // prefer these constants.
    // Health will always start at maximum. Use SetHealth() to adjust based on an integer
    // value.
    private const int MAX_HEALTH = 100;
    private const int MIN_HEALTH = 0;
    public int health = MAX_HEALTH;

    [Range(0, .3f)] public float MovementSmoothing = .05f;
    public float JumpForce = 500f;
    public float MoveForce = 25f;
    public LayerMask GroundLayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        timeController = FindObjectOfType(typeof(TimeController)) as TimeController;

        //animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeController.reversing)
        {
            Move();
        }
    }


    /// <summary>
    /// All physics related code should go here.
    /// </summary>
    private void FixedUpdate()
    {
        // Jumping
        if (cc.IsTouchingLayers(GroundLayer))
        {
            if (cc.IsTouchingLayers())
            {
                // We are on the ground, allow jumping.
                //animator.SetBool("Grounded", true);
            }
            else
            {
                // We're not on the ground, jumping should not be allowed at this time.
                //animator.SetBool("Grounded", false);
            }
        }
    }


    /// <summary>
    /// Flip the character in the opposite direction. The current direction is stored as a boolean
    /// variable, while the actual flip operation is handled by simply multiplying the scale by -1.
    /// </summary>
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    /// <summary>
    /// Moves the player based on the horizontal input axis and jumping commands.
    /// Horizontal movement is handled though collecting the current and target velocities and then 
    /// smoothly moving between them.
    /// Jumping is handled by adding upward force to the Rigidbody.
    /// </summary>
    private void Move()
    {
        float move = Input.GetAxis("Horizontal");
        Vector3 currentVelocity = Vector3.zero;
        Vector3 targetVelocity = new Vector2(move * MoveForce, rb.velocity.x);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, MovementSmoothing);

        //animator.SetFloat("HSpeed", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("VSpeed", rb.velocity.y);

        // The player is facing left but needs to face right, or vice versa
        if (move > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (move < 0 && facingRight)
        {
            FlipCharacter();
        }

        // KEY_JUMP is defined at the top of this file. Do not hardcode a key here!
        if (Input.GetKeyDown(KEY_JUMP))
        {
            /*
             * need to fix animator
            if (animator.GetBool("Grounded"))
            {
            */
            Debug.Log("Jumping!");
            rb.AddForce(new Vector2(1f, JumpForce));
            //animator.SetTrigger("Jump");
            //}
        }
    }


    /// <summary>
    /// Climb a ladder (or other block tagged as being climbable)!
    /// </summary>
    private void Climb()
    {
        float y = Input.GetAxis("Vertical");

    }


    /// <summary>
    /// The player has died. Right now this only logs to the console, but is intended to display a
    /// GUI death interface and also halt all other processing.
    /// </summary>
    private void Die()
    {
        // Double-check that the health is actually at its minimum value before we kill the player.
        if (health == MIN_HEALTH)
        {
            Debug.Log("You are dead. Not big surprise.");
        }
    }


    /// <summary>
    /// Set the player's health. <c>healthModifier</c> can be any integer value, though constraints
    /// exist in to prevent the health exceeding the upper or lower boundaries defined above.
    /// The health is not actively monitored, so it is important to only use <c>SetHealth</c> to
    /// adjust it.
    /// </summary>
    /// <param name="healthModifier"></param>
    public void SetHealth(int healthModifier)
    {
        // Check the player isn't dead before we try this one. No point setting health on a dead
        // player now, is there?
        if (health != MIN_HEALTH)
        {
            if ((health + healthModifier) >= MAX_HEALTH)
            {
                health = MAX_HEALTH;
            }
            else if ((health + healthModifier) <= MIN_HEALTH)
            {
                health = MIN_HEALTH;
                Die();
            }
            else
            {
                health = health + healthModifier;
            }
        }
    }
}