using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFollowManager : MonoBehaviour
{
    private PlayerCharacterController   player;
    private float                       moveSpeed = 1.5f;
    private Vector2                     margin = new Vector2(1, 1);
    private bool                        isFollowing = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var x = transform.position.x;

            if (isFollowing)
            {
                if (Mathf.Abs(x - player.transform.position.x) > margin.x)
                {
                    x = Mathf.Lerp(x, player.transform.position.x, moveSpeed * Time.deltaTime);
                }
            }

            transform.position = new Vector2(x, transform.position.y);
        }
    }
}
