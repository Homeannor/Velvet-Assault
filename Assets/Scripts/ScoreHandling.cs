using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandling : MonoBehaviour
{
    public GameObject player;
    private float score;
    private float timer;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Initial score setting
        score = 0f;
        timer = 0f;
        
        updateScore(0);
    }

    // Update is called once per frame
    private void Update()
    {
        updateTime();
    }

    public void updateTime()
    {
        timer += Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(timer);
        timeText.text = "TIME: " + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00") + ":" + time.Milliseconds.ToString("000");
    }

    public void updateScore(float amount)
    {
        score += amount;
        scoreText.text = "SCORE: " + score;
    }
}
