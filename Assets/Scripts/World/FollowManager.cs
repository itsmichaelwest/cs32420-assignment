using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public int                                          boundary = 1;
    public float                                        moveSpeed = 1.5f;
    protected static PlayerCharacterController          player;
    protected static Vector2                            margin = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController;
    }
}
