using UnityEngine;

public class SkyFollowManager : FollowManager
{
    void Update()
    {
        if (player)
        {
            float x = transform.position.x;
            float y = transform.position.y;

            if (Mathf.Abs(x - player.transform.position.x) > margin.x)
                x = Mathf.Lerp(x, player.transform.position.x, moveSpeed * Time.deltaTime);
            else if (Mathf.Abs(y - player.transform.position.y) > margin.y)
                y = Mathf.Lerp(y, player.transform.position.y, moveSpeed * Time.deltaTime);

            transform.position = new Vector2(x, y);
        }
    }
}
