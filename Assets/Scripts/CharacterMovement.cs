using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    private List<GameObject> _stickedObjects;
    public float speed = 0.1f;
    public float radius = 1.2f;
    private bool _isFacingRight = false;
    private long lastSneeze;

    void Start()
    {
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
        _stickedObjects = new List<GameObject>();
    }

    void FixedUpdate()
    {
        if (_animator.GetBool("isSneeze") 
            && (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - lastSneeze < 1000)
            return;
        
        CheckStickedObjects();
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

        _animator.Update(0f);
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
            SetParamToTrueAndOthersToFalse("isSneeze");
            lastSneeze = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            RemoveStickedObjects();
        }
    }

    private void RemoveStickedObjects()
    {
        foreach (GameObject gameObject in _stickedObjects)
        {
            gameObject.transform.SetParent(null);
            gameObject.GetComponent<DropMovement>().Drop();
        }

        _stickedObjects.Clear();
    }

    private void Flip()
    {
        foreach (var stickedObject in _stickedObjects)
        {
            stickedObject.transform.SetParent(null);
        }

        _isFacingRight = !_isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        
        foreach (var stickedObject in _stickedObjects)
        {
            stickedObject.transform.SetParent(_player.transform);
        }
    }

    private void CheckStickedObjects()
    {
        for(int i = 0; i < _stickedObjects.Count; i++)
        {
            float distance = Vector3.Distance(_stickedObjects[i].transform.position, _player.transform.position);
            if (Math.Abs(distance) > radius)
            {
                _stickedObjects[i].transform.SetParent(null);
                _stickedObjects.RemoveAt(i);
            }
        }
    }

    public int GetNumberOfStickedObjects()
    {
        return _stickedObjects.Count;
    }
}