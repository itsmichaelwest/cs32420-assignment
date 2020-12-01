using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    protected Transform                 thing;
    public bool                         reversing = false;
    protected LinkedList<Vector3>       positions;
    protected LinkedList<Quaternion>    rotations;
    protected const int                 MAXIMUM_REVERSE_SECS = 20;      // Maximum time allowed for reversal in seconds
    private MenuController              menu;
    private PlayerCharacterController   player;

    private LinkedList<int>             health;
    private LinkedList<int>             animator;
    private LinkedList<bool>            isFacingRight;

    public static bool                  buffersPaused = false;

    void Start()
    {
        thing = GetComponent<Transform>();
        positions = new LinkedList<Vector3>();
        rotations = new LinkedList<Quaternion>();
        menu = FindObjectOfType(typeof(MenuController)) as MenuController;
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;

        if (thing.tag == "Player")
        {
            health = new LinkedList<int>();
            animator = new LinkedList<int>();
            isFacingRight = new LinkedList<bool>();
        }
    }

    void Update()
    {
        if (!buffersPaused)
        {
            // If user is holding the rewind key, begin reversing time. Otherwise stop reversing time.
            if (Input.GetKey(Keys.REVERSE))
            {
                reversing = true;
                Time.timeScale = 2;
                player.SetIsKillable(false);
            }
            else
            {
                // This is to prevent timeScale being set to 1 before the main menu is dismissed.
                if (menu.isGameStarted())
                {
                    reversing = false;
                    Time.timeScale = 1;
                    player.SetIsKillable(true);
                }
            }

            // Clear the list of positions once the rewind key is let go, as we only store one rewind session.
            if (Input.GetKeyUp(Keys.REVERSE))
            {
                if (menu.isGameStarted())
                {
                    positions.Clear();
                    player.SetIsKillable(true);
                }
            }
        }

    }

    /// <summary>
    /// Adding and removing positions, rotation, and such is handled on each physics engine update.
    /// </summary>
    void FixedUpdate()
    {
        if (thing.tag == "Player")
        {
            PlayerUpdate();
        }
        if (!reversing)
        {
            if (positions.Count() >= (MAXIMUM_REVERSE_SECS * 60))
                positions.RemoveFirst();
            positions.AddLast(thing.position);
            if (rotations.Count() >= (MAXIMUM_REVERSE_SECS * 60))
                rotations.RemoveFirst();
            rotations.AddLast(thing.rotation);
        }
        else
        {
            if (positions.Count() != 0 && rotations.Count() != 0)
            {
                thing.position = positions.Last();
                positions.RemoveLast();
                thing.rotation = rotations.Last();
                rotations.RemoveLast();
            }
            else if (positions.Count() == 0 && rotations.Count() == 0)
            {
                if (thing.tag == "Rock")
                    Destroy(thing.gameObject);
            }
        }
    }


    /// <summary>
    /// Called if the player character is being reversed, this rewinds a few more additional parameters.
    /// </summary>
    void PlayerUpdate()
    {
        if (!reversing)
        {
            if (health.Count() >= (MAXIMUM_REVERSE_SECS * 60))
                health.RemoveFirst();
            health.AddLast(thing.gameObject.GetComponent<PlayerCharacterController>().ReturnHealth());

            if (animator.Count() > (MAXIMUM_REVERSE_SECS * 60))
                animator.RemoveFirst();
            animator.AddLast(thing.gameObject.GetComponent<PlayerCharacterController>().ReturnAnimatorState());

            if (isFacingRight.Count() > (MAXIMUM_REVERSE_SECS * 60))
                isFacingRight.RemoveFirst();
            isFacingRight.AddLast(thing.gameObject.GetComponent<PlayerCharacterController>().isFacingRight);
        }
        else
        {
            if (health.Count() != 0)
            {
                thing.gameObject.GetComponent<PlayerCharacterController>().DirectSetHealth(health.Last());
                health.RemoveLast();
            }

            if (animator.Count() != 0)
            {
                int state = animator.Last();

                switch (state)
                {
                    case 0:
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetBool("Grounded", true);
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetBool("Running", false);
                        thing.gameObject.GetComponent<PlayerCharacterController>().isGrounded = true;
                        break;
                    case 1:
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetBool("Grounded", false);
                        thing.gameObject.GetComponent<PlayerCharacterController>().isGrounded = false;
                        break;
                    case 2:
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetTrigger("Jump");
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetBool("Grounded", false);
                        thing.gameObject.GetComponent<PlayerCharacterController>().isGrounded = false;
                        break;
                    case 3:
                        thing.gameObject.GetComponent<PlayerCharacterController>().AnimatorSetBool("Running", true);
                        break;
                    default:
                        break;
                }

                animator.RemoveLast();
            }

            if (isFacingRight.Count() != 0)
            {
                if (isFacingRight.Last() == true)
                    thing.gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                else
                    thing.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                isFacingRight.RemoveLast();
            }
        }
    }
}