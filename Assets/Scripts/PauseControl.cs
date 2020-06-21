using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public static bool paused = false;
    public Button resumeButton;
    public Button soundToggle;
    public Button quitButton;
    public GameObject canvas;
    public AudioSource sound;
    
    private string _soundOn = "Sound ON";
    private string _soundOFF = "Sound OFF";
    private bool _isSound = true;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (resumeButton != null && quitButton != null)
        {
            quitButton.onClick.AddListener(Quit);
            resumeButton.onClick.AddListener(Resume);
            soundToggle.onClick.AddListener(SoundToggle);
        }
    }

    void Update()
    {
        Vector3 position = _player.transform.position;
        position.z = canvas.transform.position.z;
        canvas.transform.position = position;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (!paused)
            {
                canvas.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0f;
                canvas.SetActive(true);
            }
        }
    }

    void SoundToggle()
    {
        _isSound = !_isSound;
        if (!_isSound)
        {
            sound.Pause();
            soundToggle.GetComponentInChildren<Text>().text = _soundOFF;
        }
        else
        {
            sound.Play();
            soundToggle.GetComponentInChildren<Text>().text = _soundOn;
        }
    }

    void Resume()
    {
        paused = false;
        canvas.SetActive(false);
    }

    void Quit()
    {
        Application.Quit();
    }
}