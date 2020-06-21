using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public static bool paused = false;
    public Button resumeButton;
    public Button saveButton;
    public Button quitButton;
    public GameObject canvas;

    private void Start()
    {
        if (resumeButton != null && saveButton != null && quitButton != null)
        {
            quitButton.onClick.AddListener(Quit);
            saveButton.onClick.AddListener(Save);
            resumeButton.onClick.AddListener(Resume);
        }
    }

    void Update()
    {
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

    void Save()
    {
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