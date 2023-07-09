using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField] private Text timerText;
    [SerializeField] private float timeLimit = 120f;
    private float timeRemaining = 30f;
    [SerializeField] private string comicSceneName;

    private bool comicSceneLoadStarted = false;
    public Hourglass hourglass;


    void Start() {
        // timeRemaining = GetComponent<AudioSource>().clip.length;
        hourglass.Init(timeRemaining);
    }

    void Update() {
        timeRemaining = Mathf.Max(timeRemaining - Time.deltaTime, 0);

        timerText.text = $"{timeRemaining.ToString("00.00")}s";

        if (timeRemaining <= 0 && !comicSceneLoadStarted) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(comicSceneName);
            comicSceneLoadStarted = true;
        }
    }

}
