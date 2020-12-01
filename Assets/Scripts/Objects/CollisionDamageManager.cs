using UnityEngine;

public class CollisionDamageManager : MonoBehaviour
{
    public int                          damage = 100;
    private PlayerCharacterController   player;

    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player.DecreaseHealth(damage);
    }

    private void OnDestroy()
    {
        transform.parent.GetComponent<RockFallingController>().DecreaseRockCount(1);
    }
}
