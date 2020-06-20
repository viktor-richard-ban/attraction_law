using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject otherLever;
    public bool isDisabled;

    // Start is called before the first frame update
    void Start()
    {

        if(isDisabled) {
            this.gameObject.SetActive(false);
            otherLever.SetActive(true);
        }else {
            this.gameObject.SetActive(true);
            otherLever.SetActive(false);
        }
    }

}
