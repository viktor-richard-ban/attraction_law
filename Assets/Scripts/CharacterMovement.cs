using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject player;
    public float speed = 0.1f;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            playerPos.y += speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerPos.y -= speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerPos.x -= speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerPos.x += speed;
        }

        player.transform.position = playerPos;
    }
}
