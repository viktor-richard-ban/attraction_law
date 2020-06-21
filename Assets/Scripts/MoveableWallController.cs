using UnityEngine;

public class MoveableWallController : MonoBehaviour
{
    public GameObject hider;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hider.SetActive(false);
        }
    }
}