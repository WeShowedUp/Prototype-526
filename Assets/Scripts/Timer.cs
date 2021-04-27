using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Text Score;
    private float startTime;
    private bool end;
  
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (end)
            return;
        float t = Time.time - startTime;
        string min = ((int)t / 60).ToString();
        string sec = ((int)t % 60).ToString("D2");

        timerText.text = min + ":" + sec;
        Score.text = "Score "+min + ":" + sec;
    }

    public void setEnd()
    {
        end = true;
    }

    public void setResume()
    {
        end = false;
    }
}
