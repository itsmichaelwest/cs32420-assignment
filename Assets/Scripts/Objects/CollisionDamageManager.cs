using UnityEngine;

public class CollisionDamageManager : MonoBehaviour
{
    private PlayerCharacterController   player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // This has the effect of killing the player
            player.DecreaseHealth(100);
        }
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<RockFallingController>().DecreaseRockCount(1);
    }
}
