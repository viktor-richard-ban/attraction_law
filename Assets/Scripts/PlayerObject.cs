using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    public int keyCounter = 0;
    bool canGet = false;
    bool canOpen = false;
    Collision2D savedCollision = null;
    
    public AudioSource lockSrc;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && this.canGet && savedCollision.gameObject.tag=="Key")
        {
            savedCollision.gameObject.SetActive(false);
            keyCounter+=1;
            this.canGet = false;
        }else if(Input.GetKeyDown(KeyCode.E) && this.canOpen && savedCollision.gameObject.tag=="Door"){
            savedCollision.gameObject.SetActive(false);
            lockSrc.Play();
            keyCounter-=1;
            this.canOpen = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Key"){
            this.canGet = true;
            savedCollision = col;
        }else if(col.gameObject.tag == "Door" && keyCounter>0) {
            this.canOpen = true;
            savedCollision = col;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        savedCollision = null;
        this.canGet = false;
        this.canOpen = false;
    }
}
