using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureCollector : MonoBehaviour
{

    public int items = 0;
    public AudioSource switchOn;
    public AudioSource switchOff;

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
        switchOn.Play();
        items+=1;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        switchOff.Play();
        items-=1;
    }

}
