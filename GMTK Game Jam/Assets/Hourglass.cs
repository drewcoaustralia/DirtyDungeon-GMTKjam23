using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hourglass : MonoBehaviour
{
    public Image top;
    public Image bottom;
    private float totalTime;
    private float timeRemaining;

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        top.fillAmount = Mathf.Clamp(timeRemaining/totalTime, 0f, 1f);
        bottom.fillAmount = 1f-top.fillAmount;
    }

    public void Init(float time)
    {
        totalTime = time;
        timeRemaining = time;
    }
}
