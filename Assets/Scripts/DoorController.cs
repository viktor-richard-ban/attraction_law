using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject firstLever;
    public GameObject secondLever;
    public GameObject thirdLever;
    public GameObject fourthLever;

    public bool canOpen = false;
    bool thereiam = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame .)
    void Update()
    {
        if (!firstLever.active && secondLever.active && !thirdLever.active && !fourthLever.active)
        {
            canOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && this.canOpen && this.thereiam)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        this.thereiam = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        this.thereiam = false;
    }
}