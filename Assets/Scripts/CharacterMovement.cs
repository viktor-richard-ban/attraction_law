using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    public float speed = 0.1f;

    void Start()
    {
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector3 playerPos = _player.transform.position;

        _animator.enabled = false;
        if (Input.GetKey(KeyCode.W))
        {
            SetParamToTrueAndOthersToFalse("isMoveAway");
            playerPos.y += speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetParamToTrueAndOthersToFalse("isMoveForward");
            playerPos.y -= speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SetParamToTrueAndOthersToFalse("isMoveSideLeft");
            playerPos.x -= speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            SetParamToTrueAndOthersToFalse("isMoveSideRight");
            playerPos.x += speed;
        }
        else
        {
            SetParamToTrueAndOthersToFalse("isIdle");
        }
        _animator.enabled = true;
        _player.transform.position = playerPos;
    }

    private void SetParamToTrueAndOthersToFalse(string name)
    {
        if (_animator.GetBool(name)) return;
        _animator.SetBool(name, true);
        foreach (AnimatorControllerParameter param in _animator.parameters)
        {
            if (param.name.Equals(name)) continue;
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                _animator.SetBool(param.name, false);
            }
        }
    }
}