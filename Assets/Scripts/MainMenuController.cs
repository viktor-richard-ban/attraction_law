using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button playButton;
    public Button creditButton;
    public Button quitButton;

    void Start()
    {
        playButton = playButton.GetComponent<Button>();
        creditButton = creditButton.GetComponent<Button>();

        playButton.onClick.AddListener(onPlayButtonClick);
        creditButton.onClick.AddListener(onCreditButtonClick);
        quitButton.onClick.AddListener(onQuitButtonClick);
    }

    void onPlayButtonClick()
    {
        SceneManager.LoadScene(1);
        Scene nextScene = SceneManager.GetSceneByName("Test1");
        if (nextScene.isLoaded)
        {
            Scene active = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(active.buildIndex);
            SceneManager.SetActiveScene(nextScene);
        }
    }

    void onCreditButtonClick()
    {
        
    }

    void onQuitButtonClick()
    {
        Application.Quit();
    }
}