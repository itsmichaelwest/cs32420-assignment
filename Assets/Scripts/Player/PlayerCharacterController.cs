using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private Animator                    animator;
    private Rigidbody2D                 rb2d;
    private PolygonCollider2D           pc2d;
    private TimeController              timeController;

    // Do not use numbers when referring to the maximum or minimum health values, instead
    // prefer these constants.
    // Health will always start at maximum. Use SetHealth() to adjust based on an integer
    // value.
    private const int                   MAX_HEALTH = 100;
    private const int                   MIN_HEALTH = 0;
    protected int                       health = MAX_HEALTH;

    [Range(0, .3f)] public float        MovementSmoothing = .05f;
    public float                        JumpForce = 11f;
    public float                        MoveForce = 7.5f;
    public LayerMask                    GroundLayer;

    // Mutually exclusive player states
    private bool                        isFacingRight = true;
    private bool                        isGrounded = true;
    private bool                        isKillable = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        timeController = GetComponent<TimeController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!timeController.reversing)
            Move();
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
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
            isFacingRight = !isFacingRight;
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            isFacingRight = !isFacingRight;
        }

        rb2d.velocity = new Vector2(move * MoveForce, rb2d.velocity.y);
        animator.SetFloat("VSpeed", rb2d.velocity.y);

        // KEY_JUMP is defined at the top of this file. Do not hardcode a key here!
        if (Input.GetKeyDown(Keys.JUMP) && isGrounded)
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
                GameOverMenuController.GameOver();
            }
        }
    }


    /// <summary>
    /// Increase the player's health by the amount specified.
    /// </summary>
    /// <param name="healthModifier"></param>
    public void IncreaseHealth(int healthModifier)
    {
        if (health != MAX_HEALTH)
        {
            if ((health + healthModifier) <= MAX_HEALTH)
            {
                health = MAX_HEALTH;
            }
            else
            {
                health =+ healthModifier;
            }
        }
    }


    /// <summary>
    /// Decrease the player's health by the amount specified.
    /// </summary>
    /// <param name="healthModifier"></param>
    public void DecreaseHealth(int healthModifier)
    {
        if (health != MIN_HEALTH)
        {
            if ((health - healthModifier) <= MIN_HEALTH)
            {
                health = MIN_HEALTH;
                Die();
            }
            else
            {
                health =- healthModifier;
            }
        }
    }


    /// <summary>
    /// Fetch the player's health.
    /// </summary>
    /// <returns></returns>
    public int ReturnHealth() { return health; }


    /// <summary>
    /// Set if the player can be killed by damage objects in the scene.
    /// </summary>
    /// <param name="killable">Set to true if the player can be killed, false if not.</param>
    public void SetIsKillable(bool killable) { isKillable = killable; }
}