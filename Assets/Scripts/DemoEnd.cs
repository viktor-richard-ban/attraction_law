using UnityEngine;

public class DemoEnd : MonoBehaviour
{
    public GameObject demoEnd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            demoEnd.SetActive(true);
        }
    }
}