using UnityEngine;

public class SkyFollowManager : FollowManager
{
    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var x = transform.position.x;
            var y = transform.position.y;

            if (Mathf.Abs(x - player.transform.position.x) > margin.x)
            {
                x = Mathf.Lerp(x, player.transform.position.x, moveSpeed * Time.deltaTime);
            }
            else if (Mathf.Abs(y - player.transform.position.y) > margin.y)
            {
                y = Mathf.Lerp(y, player.transform.position.y, moveSpeed * Time.deltaTime);
            }

            transform.position = new Vector2(x, y);
        }
    }
}
