using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLogic : MonoBehaviour {
    void Start() {

    }

    public void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
