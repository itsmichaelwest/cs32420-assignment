using UnityEngine;

public class CameraFollowManager : FollowManager
{
    private Camera      cam;
    private Vector2     screenBounds;

    void Start()
    {
        cam = GetComponent<Camera>();
        screenBounds = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (Mathf.Abs(x - player.transform.position.x) > margin.x)
            if ((player.transform.position.x > screenBounds.x - boundary) || (player.transform.position.x < 0 + boundary))
                x = Mathf.Lerp(x, player.transform.position.x, moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - player.transform.position.y) > margin.y)
            if ((player.transform.position.y > screenBounds.y - boundary) || (player.transform.position.y < 0 + boundary))
                y = Mathf.Lerp(y, player.transform.position.y, moveSpeed * Time.deltaTime);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}