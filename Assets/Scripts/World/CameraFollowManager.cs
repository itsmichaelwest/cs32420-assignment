using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowManager : FollowManager
{
    private Camera      cam;
    private Vector2     screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        screenBounds = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        float cameraX = transform.position.x;
        float cameraY = transform.position.y;

        if (Mathf.Abs(cameraX - player.transform.position.x) > margin.x)
        {
            if ((player.transform.position.x > screenBounds.x - boundary) || (player.transform.position.x < 0 + boundary))
            {
                cameraX = Mathf.Lerp(cameraX, player.transform.position.x, moveSpeed * Time.deltaTime);
            }
        }
        if (Mathf.Abs(transform.position.y - player.transform.position.y) > margin.y)
        {
            if ((player.transform.position.y > screenBounds.y - boundary) || (player.transform.position.y < 0 + boundary))
            {
                cameraY = Mathf.Lerp(cameraY, player.transform.position.y, moveSpeed * Time.deltaTime);
            }
        }

        // Update the camera's position
        transform.position = new Vector3(cameraX, cameraY, transform.position.z); // Smoothly move the camera to the player's position, we do this a bit faster than movement
    }
}