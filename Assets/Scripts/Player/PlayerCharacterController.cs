using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private Animator                    animator;
    private Rigidbody2D                 rb2d;
    private PolygonCollider2D           pc2d;
    private TimeController              timeController;

    // Do not use numbers when referring to the maximum or minimum health values, instead
    // prefer these constants.
    private const int                   MAX_HEALTH = 100;
    private const int                   MIN_HEALTH = 0;
    protected int                       health = MAX_HEALTH;

    [Range(0, .3f)] public float        MovementSmoothing = .05f;
    public float                        JumpForce = 11f;
    public float                        MoveForce = 7.5f;
    public LayerMask                    GroundLayer;
    public int                          FallOffBoundary = 15;

    // Mutually exclusive player states
    public bool                         isFacingRight = true;
    public bool                         isGrounded = true;
    public bool                         isKillable = true;
    private int                         AnimatorState = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        timeController = GetComponent<TimeController>();
    }

    void Update()
    {
        if (!timeController.reversing)
            Move();
            CheckPosition();
            CheckHealth();
    }


    /// <summary>
    /// Moves the player based on the horizontal input axis and jumping commands.
    /// Horizontal movement is handled though collecting the current and target velocities and then 
    /// smoothly moving between them. Jumping is handled by adding upward force to the Rigidbody.
    /// </summary>
    private void Move()
    {
        if (!isGrounded && pc2d.IsTouchingLayers(GroundLayer))
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
            AnimatorState = 0;
        }

        if (isGrounded && !pc2d.IsTouchingLayers(GroundLayer))
        {
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
            AnimatorState = 1;
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

        if (Input.GetKeyDown(Keys.JUMP) && isGrounded)
        {
            animator.SetTrigger("Jump");
            AnimatorState = 2;
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
        }
        else if (Mathf.Abs(move) > Mathf.Epsilon)
        {
            animator.SetBool("Running", true);
            AnimatorState = 3;
        }
        else
        {
            animator.SetBool("Running", false);
            AnimatorState = 0;
        }
    }


    /// <summary>
    /// The player has died. Display the GUI death interface and also halt all other processing.
    /// </summary>
    private void Die()
    {
        if (health == MIN_HEALTH)
            if (isKillable)
            {
                animator.SetTrigger("Death");
                GameOverMenuController.GameOver();
            }
    }


    /// <summary>
    /// If the player falls out of the world, they should die.
    /// </summary>
    private void CheckPosition()
    {
        if (transform.position.y <= (Mathf.Abs(FallOffBoundary) * (-1)))
            Die();
    }


    /// <summary>
    /// Check the player's current health, and if it rises above minimum, play the
    /// recovery animation.
    /// </summary>
    private void CheckHealth()
    {
        if (health > MIN_HEALTH)
            animator.SetTrigger("Recover");
    }


    /// <summary>
    /// Increase the player's health by the amount specified.
    /// </summary>
    public void IncreaseHealth(int healthModifier)
    {
        if (health != MAX_HEALTH)
            if ((health + healthModifier) <= MAX_HEALTH)
            {
                health = MAX_HEALTH;
            }
            else
                health =+ healthModifier;
    }


    /// <summary>
    /// Decrease the player's health by the amount specified.
    /// </summary>
    public void DecreaseHealth(int healthModifier)
    {
        if (health != MIN_HEALTH)
            if ((health - healthModifier) <= MIN_HEALTH)
            {
                health = MIN_HEALTH;
                Die();
            }
            else
                health =- healthModifier;
    }


    /// <summary>
    /// Fetch the player's health.
    /// </summary>
    public int ReturnHealth() { return health; }


    /// <summary>
    /// Directly set the player's health to an integer value. Use of this method is strongly
    /// discourged, prefer using <code>IncreaseHealth</code> and <code>DecreaseHealth</code>.
    /// </summary>
    public void DirectSetHealth(int setHealth) { health = setHealth; }


    /// <summary>
    /// Set if the player can be killed by damage objects in the scene.
    /// </summary>
    public void SetIsKillable(bool killable) { isKillable = killable; }


    /// <summary>
    /// Return the current state of the animator. Only used for time reversal processing,
    /// the output is meaningless if you are trying to get the animator's actual state.
    /// </summary>
    public int ReturnAnimatorState() { return AnimatorState; }


    /// <summary>
    /// Set a boolean value on the animator. This method exists purely to allow the time
    /// reversal process to control the animator, prefer controling it directly where possible.
    /// </summary>
    public void AnimatorSetBool(string var, bool b) { animator.SetBool(var, b); }


    /// <summary>
    /// Set a trigger on the animator. This method exists purely to allow the time
    /// reversal process to control the animator, prefer controling it directly where possible.
    /// </summary>
    public void AnimatorSetTrigger(string var) { animator.SetTrigger(var); }
}