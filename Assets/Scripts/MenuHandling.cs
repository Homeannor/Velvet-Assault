using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandling : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI finalTimerText;

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        rankText.text = PlayerPrefs.GetString("FinalRank");
        finalTimerText.text = PlayerPrefs.GetString("FinalTime");
    }

    void Update()
    {
        rankText.text = PlayerPrefs.GetString("FinalRank");
        finalTimerText.text = PlayerPrefs.GetString("FinalTime");
    }
}
