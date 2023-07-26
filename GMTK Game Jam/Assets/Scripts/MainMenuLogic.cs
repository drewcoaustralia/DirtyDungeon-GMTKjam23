using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour {
    [SerializeField] private string gameSceneName;

    public void HandlePlayButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void HandleOptionsButton() {
        Debug.Log("Options");
    }

    public void HandleQuitButton() {
        Application.Quit();
    }
}
