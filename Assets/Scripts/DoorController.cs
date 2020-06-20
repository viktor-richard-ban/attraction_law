using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject[] levers;
    public string pattern;
    private GameObject _door;
    void Start()
    {
        _door = GetComponent<GameObject>();
    }

    void FixedUpdate()
    {
        if (pattern.Length != levers.Length)
        {
            Debug.LogError("There should be as many levers as the length of the pattern");
            return;
        }

        if (CheckPattern())
        {
            _door.SetActive(false);
            return;
        }
        
        _door.SetActive(true);
    }

    bool CheckPattern()
    {
        bool match = true;
        for(int i = 0; i < pattern.Length; i++)
        {
            match &= (pattern[i] == '1' && levers[i].GetComponent<LeverController>().isOn)
                     || (pattern[i] == '0' && !levers[i].GetComponent<LeverController>().isOn);
            Debug.Log("Is Match? " + match);
        }

        return match;
    }
}
