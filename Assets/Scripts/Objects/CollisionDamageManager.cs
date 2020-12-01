using UnityEngine;

public class CollisionDamageManager : MonoBehaviour
{
    private PlayerCharacterController   player;

    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player.DecreaseHealth(100);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<RockFallingController>().DecreaseRockCount(1);
    }
}
