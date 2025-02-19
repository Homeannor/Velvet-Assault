using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandling : MonoBehaviour
{
    public GameObject player;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Initial score setting
        score = 0f;
        gameObject.GetComponent<TextMeshProUGUI>().text = "[SCORE // " + score + "]";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScore(float amount)
    {
        score += amount;
        gameObject.GetComponent<TextMeshProUGUI>().text = "[SCORE // " + score + "]";
    }
}
