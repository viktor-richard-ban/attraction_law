using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject firstLever;
    public GameObject secondLever;
    public GameObject thirdLever;
    public GameObject fourthLever;

    public bool canOpen = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!firstLever.active && secondLever.active && !thirdLever.active && !fourthLever.active){
            canOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && this.canOpen)
        {
            this.gameObject.SetActive(false);
        }
    }
}
