using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    // Camera target, this will be the player
    public Transform player;

    // 2D box collider, this will be the bounds for the camera
    //public BoxCollider2D cameraBoundary;

    // If the player remains within a 1x1 square, the camera won't bother moving.
    // This prevents micromovements.
    public Vector2 margin = new Vector2(1, 1);

    private Vector3 minmum, maximum;

    private float moveSpeed = 1.5f;
    private bool isFollowing = true;

    // Start is called before the first frame update
    void Start()
    {
        //minmum = cameraBoundary.bounds.min;
        //maximum = cameraBoundary.bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player exists, otherwise it is useless having the camera follow
        if (player)
        {
            // Get camera's current position
            var cameraX = transform.position.x;
            var cameraY = transform.position.y;

            if (isFollowing)
            {
                if (Mathf.Abs(cameraX - player.position.x) > margin.x)
                {
                    cameraX = Mathf.Lerp(cameraX, player.position.x, moveSpeed * Time.deltaTime);
                }

                if (Mathf.Abs(cameraY - player.position.y) > margin.y)
                {
                    cameraY = Mathf.Lerp(cameraY, player.position.y, moveSpeed * Time.deltaTime);
                }
            }

            // Update the camera's position
            transform.position = new Vector3(cameraX, cameraY, transform.position.z); // Smoothly move the camera to the player's position, we do this a bit faster than movement
        }
    }
}