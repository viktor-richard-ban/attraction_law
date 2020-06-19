using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    private List<GameObject> _stickedObjects;
    public float speed = 0.1f;
    private bool _isFacingRight = false;

    void Start()
    {
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
        _stickedObjects = new List<GameObject>();
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
            if (_isFacingRight) Flip();
            SetParamToTrueAndOthersToFalse("isMoveSide");
            playerPos.x -= speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!_isFacingRight) Flip();
            SetParamToTrueAndOthersToFalse("isMoveSide");
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodThing"))
        {
            other.transform.SetParent(_player.transform);
            _stickedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ResetBush"))
        {
            RemoveStickedObjects();
        }
    }

    private void RemoveStickedObjects()
    {
        Debug.Log(_stickedObjects.Count);
        foreach (GameObject gameObject in _stickedObjects)
        {
            gameObject.transform.SetParent(null);
        }

        _stickedObjects.Clear();
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}