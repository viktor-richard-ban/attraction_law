using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    int keyCounter = 0;
    bool canGet = false;
    Collision2D savedCollision = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.canGet == true)
        {
            savedCollision.gameObject.SetActive(false);
            keyCounter+=1;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Key"){
            savedCollision = col;
            this.canGet = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        savedCollision = null;
        this.canGet = false;
    }
}
