using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCharacterController : MonoBehaviour
{
    private Animator                    animator;
    private Rigidbody2D                 rb2d;
    private CircleCollider2D            cc;
    private PolygonCollider2D           pc2d;

    private const string                KEY_JUMP = "space";

    private TimeController              timeController;
    private MenuController              menuController;

    // Do not use numbers when referring to the maximum or minimum health values, instead
    // prefer these constants.
    // Health will always start at maximum. Use SetHealth() to adjust based on an integer
    // value.
    private const int                   MAX_HEALTH = 100;
    private const int                   MIN_HEALTH = 0;
    public int                          health = MAX_HEALTH;

    [Range(0, .3f)] public float        MovementSmoothing = .05f;
    public float                        JumpForce = 10f;
    public float                        MoveForce = 15f;
    public LayerMask                    GroundLayer;

    private bool                        isFacingRight = false;
    private bool                        isGrounded = true;
    private bool                        isKillable = true;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        timeController = GetComponent<TimeController>();

        menuController = FindObjectOfType(typeof(MenuController)) as MenuController;
    }


    // Update is called once per frame
    void Update()
    {
        if (!timeController.reversing)
            Move();
    }


    /// <summary>
    /// Flip the character in the opposite direction. The current direction is stored as a boolean
    /// variable, while the actual flip operation is handled by simply multiplying the scale by -1.
    /// </summary>
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
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
        if (!isGrounded && pc2d.IsTouchingLayers(GroundLayer))
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
        }

        if (isGrounded && !pc2d.IsTouchingLayers(GroundLayer))
        {
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
        }

        float move = Input.GetAxis("Horizontal");

        // Flip the character to face the right direction
        if (move > 0)
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        else if (move < 0)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);


        // Character core movement
        //Vector3 currentVelocity = Vector3.zero;
        //Vector3 targetVelocity = new Vector2(move * MoveForce, rb2d.velocity.x);
        //rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, targetVelocity, ref currentVelocity, MovementSmoothing);


        rb2d.velocity = new Vector2(move * MoveForce, rb2d.velocity.y);
        animator.SetFloat("VSpeed", rb2d.velocity.y);

        // KEY_JUMP is defined at the top of this file. Do not hardcode a key here!
        if (Input.GetKeyDown(KEY_JUMP) && isGrounded)
        {
            animator.SetTrigger("Jump");
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
        }
        else if (Mathf.Abs(move) > Mathf.Epsilon)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);
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
            // Obviously we cannot kill the player if they are in a state where killing is disallowed.
            if (isKillable)
            {
                animator.SetTrigger("Death");
                menuController.ShowGameOverMenu();
            }
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


    public void SetIsKillable(bool killable)
    {
        isKillable = killable;
    }
}