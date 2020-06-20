using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleStick : MonoBehaviour
{
    public bool isOn = false;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_animator.GetBool("isOn") && !isOn)
        {
            _animator.SetBool("isOn", false);
        }
        else if (!_animator.GetBool("isOn") && isOn)
        {
            _animator.SetBool("isOn", true);
        }
    }
}