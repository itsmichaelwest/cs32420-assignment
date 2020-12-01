using UnityEngine;

public class RockFallingController : MonoBehaviour
{
    public GameObject       rock;
    private const int       MAX_ROCK_SPAWNS = 10;       // Adjust the maximum number of rocks to spawn.
    private int             rockCount = 0;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if ((hit.collider.gameObject.tag == "Player") && (rockCount <= MAX_ROCK_SPAWNS))
        {
            GameObject child = Instantiate(rock, transform.position, Quaternion.identity);
            child.transform.parent = transform;
            rockCount++;
        }
    }

    /// <summary>
    /// Decrease the count of rocks that have been spawned. This is exclusively used by the
    /// time reversal processing to remove rocks from the count once they have been destroyed.
    /// </summary>
    public void DecreaseRockCount(int count)
    {
        rockCount = rockCount - count;
    }
}