using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public Text timerText;
    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f1");
        timerText.text = minutes + ":" + seconds;
    }
}