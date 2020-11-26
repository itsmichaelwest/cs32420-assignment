using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public int                          boundary = 1;
    public float                        moveSpeed = 1.5f;

    private Camera                      cam;
    private PlayerCharacterController   player;
    private Vector2                     margin = new Vector2(1, 1);
    private bool                        isFollowing = true;
    private Vector2                     screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
        cam = GetComponent<Camera>();

        screenBounds = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player exists, otherwise it is useless having the camera follow
        if (player)
        {
            float cameraX = transform.position.x;
            float cameraY = transform.position.y;

            if (isFollowing)
            {
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
            }

            // Update the camera's position
            transform.position = new Vector3(cameraX, cameraY, transform.position.z); // Smoothly move the camera to the player's position, we do this a bit faster than movement
        }
    }
}