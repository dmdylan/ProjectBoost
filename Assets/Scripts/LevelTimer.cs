using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private Text text;
    private float levelTimer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = levelTimer.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
        text.text = FormatTime(levelTimer);        
    }

    string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = String.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
        return timeText;
    }
}
