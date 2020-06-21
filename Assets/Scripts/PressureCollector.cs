using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureCollector : MonoBehaviour
{

    public int items = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        items+=1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        items-=1;
    }

}
