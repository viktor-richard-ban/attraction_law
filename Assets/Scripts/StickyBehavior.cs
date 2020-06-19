using UnityEngine;

public class StickyBehavior : MonoBehaviour
{
    private GameObject stickyBody;
    void Start()
    {
        stickyBody = GameObject.Find("StickyBody");
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodThing"))
        {
            Debug.Log("Collide");
            other.transform.SetParent(stickyBody.transform);
        } 
    }
}
