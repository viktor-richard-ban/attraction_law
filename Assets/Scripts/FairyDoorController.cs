using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyDoorController : MonoBehaviour
{

    public GameObject leftTop;
    public GameObject leftBottom;
    public GameObject rightTop;
    public GameObject rightBottom;

    public AudioSource lockSrc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leftTop.GetComponent<PressureCollector>().items == 3 &&
            leftBottom.GetComponent<PressureCollector>().items == 1 &&
            rightTop.GetComponent<PressureCollector>().items == 2 &&
            rightBottom.GetComponent<PressureCollector>().items == 3) { this.gameObject.SetActive(false); lockSrc.Play(); }
            
    }
}
