using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverCollision : MonoBehaviour
{
    public GameObject other;
    public bool canPull = false;

    void Start() {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(this.gameObject.name);
        this.canPull = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        Debug.Log("Exit");
        this.canPull = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && this.canPull == true)
        {
            this.gameObject.SetActive(false);
            other.SetActive(true);
        }
    }

}
