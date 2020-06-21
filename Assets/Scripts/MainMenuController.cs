using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(onPlayButtonClick);
        quitButton.onClick.AddListener(onQuitButtonClick);
    }

    void onPlayButtonClick()
    {
        SceneManager.LoadScene(1);
        Scene nextScene = SceneManager.GetSceneByName("FirstScene");
        if (nextScene.isLoaded)
        {
            Scene active = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(active.buildIndex);
            SceneManager.SetActiveScene(nextScene);
        }
    }

    void onQuitButtonClick()
    {
        Application.Quit();
    }
}