using UnityEngine;

public class FollowManager : MonoBehaviour
{
    public int                                          boundary = 1;
    public float                                        moveSpeed = 1.5f;
    protected static PlayerCharacterController          player;
    protected static Vector2                            margin = new Vector2(1, 1);

    void Start() { player = FindObjectOfType(typeof(PlayerCharacterController)) as PlayerCharacterController; }
}
