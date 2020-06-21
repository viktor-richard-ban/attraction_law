using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    private List<GameObject> _stickedObjects;
    public float speed = 0.1f;
    public float radius = 1.2f;
    private bool _isFacingRight = false;
    private long lastSneeze;
    public bool isSticky = true;

    public AudioSource walkAudioSrc;
    public AudioSource sneezeAudioSrc;
    public AudioSource bushShakingAudioSrc;
    public AudioSource barelBrake;

    public GameObject keyDoorDialog;
    public GameObject leverDoorDialog;
    public GameObject pressurePlateDoorDialog;

    private long leverDoorDialogShowedAt;
    private long keyDoorDialogShowedAt;
    private long pressurePlateDoorDialogShowedAt;
    private float offsetY = 2f;

    void Start()
    {
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
        _stickedObjects = new List<GameObject>();
    }

    void FixedUpdate()
    {
        if (PauseControl.paused) return;
        
        if (_animator.GetBool("isSneeze")
            && (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - lastSneeze < 1000)
            return;

        CheckStickedObjects();
        Vector3 playerPos = _player.transform.position;
        long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        _animator.enabled = false;
        if (Input.GetKey(KeyCode.W))
        {
            SetParamToTrueAndOthersToFalse("isMoveAway");
            playerPos.y += speed;
            if (!walkAudioSrc.isPlaying)
                walkAudioSrc.Play();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetParamToTrueAndOthersToFalse("isMoveForward");
            playerPos.y -= speed;
            if (!walkAudioSrc.isPlaying)
                walkAudioSrc.Play();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (_isFacingRight) Flip();
            SetParamToTrueAndOthersToFalse("isMoveSide");
            playerPos.x -= speed;
            if (!walkAudioSrc.isPlaying)
                walkAudioSrc.Play();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!_isFacingRight) Flip();
            SetParamToTrueAndOthersToFalse("isMoveSide");
            playerPos.x += speed;
            if (!walkAudioSrc.isPlaying)
                walkAudioSrc.Play();
        }
        else
        {
            SetParamToTrueAndOthersToFalse("isIdle");
            walkAudioSrc.Stop();
        }

        _animator.enabled = true;
        _player.transform.position = playerPos;

        if (keyDoorDialog != null && leverDoorDialog != null && pressurePlateDoorDialog != null)
        {
            playerPos.y += offsetY;
            keyDoorDialog.transform.position = playerPos;
            leverDoorDialog.transform.position = playerPos;
            pressurePlateDoorDialog.transform.position = playerPos;
        }

        if (now - leverDoorDialogShowedAt > 2000)
            leverDoorDialog.SetActive(false);

        if (now - keyDoorDialogShowedAt > 2000)
            keyDoorDialog.SetActive(false);
        
        if (now - pressurePlateDoorDialogShowedAt > 2000)
            pressurePlateDoorDialog.SetActive(false);
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
        if (other.gameObject.CompareTag("WoodThing") && isSticky)
        {
            other.transform.SetParent(_player.transform);
            _stickedObjects.Add(other.gameObject);
            if (speed > 0.04f)
            {
                speed -= 0.02f;
                walkAudioSrc.pitch -= 0.16f;
            }
            else
            {
                speed = 0.04f;
                walkAudioSrc.pitch = 2.25f;
            }

            barelBrake.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("WoodThing"))
        {
            if (speed < 0.1f)
            {
                speed += 0.02f;
                walkAudioSrc.pitch += 0.16f;
            }
            else
            {
                speed = 0.1f;
                walkAudioSrc.pitch = 3f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ResetBush"))
        {
            sneezeAudioSrc.Play();
            bushShakingAudioSrc.Play();

            SetParamToTrueAndOthersToFalse("isSneeze");
            lastSneeze = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            RemoveStickedObjects();
        }

        if (other.gameObject.CompareTag("LeverDoorTrigger"))
        {
            leverDoorDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            leverDoorDialog.SetActive(true);
        }

        if (other.gameObject.CompareTag("KeyDoorTrigger") && GetComponent<PlayerObject>().keyCounter == 0)
        {
            keyDoorDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            keyDoorDialog.SetActive(true);
        }

        if (other.gameObject.CompareTag("PressurePlateTrigger"))
        {
            pressurePlateDoorDialogShowedAt = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            pressurePlateDoorDialog.SetActive(true);
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
        for (int i = 0; i < _stickedObjects.Count; i++)
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