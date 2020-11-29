using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFollowManager : FollowManager
{
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var x = transform.position.x;

            if (Mathf.Abs(x - player.transform.position.x) > margin.x)
            {
                x = Mathf.Lerp(x, player.transform.position.x, moveSpeed * Time.deltaTime);
            }

            transform.position = new Vector2(x, transform.position.y);
        }
    }
}
