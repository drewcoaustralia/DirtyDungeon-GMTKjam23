using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField] private Text timerText;
    [SerializeField] private float timeLimit = 150f;
    private float timeRemaining = 150f;
    [SerializeField] private string comicSceneName;

    private bool comicSceneLoadStarted = false;
    public Hourglass hourglass;


    void Start() {
        // timeRemaining = GetComponent<AudioSource>().clip.length;
        timeRemaining = timeLimit;
        hourglass.Init(timeRemaining);
    }

    void Update() {
        timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);

        timerText.text = $"{timeRemaining.ToString("00.00")}s";
        if (timeRemaining <= 0) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        // if (timeRemaining <= 0 && !comicSceneLoadStarted) {
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(comicSceneName);
        //     comicSceneLoadStarted = true;
        // }
    }

}
