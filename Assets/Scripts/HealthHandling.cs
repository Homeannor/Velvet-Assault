using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthHandling : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
    }

    Color RGB(int r, int g, int b, float a = 1f)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a);
    }

    public void updateHealth()
    {
        float currentHealth = player.GetComponent<PlayerMovement>().currentHealth;
        float maxHealth = player.GetComponent<PlayerMovement>().maxHealth;
        float healthPercentage = (currentHealth / maxHealth);
        healthPercentage = Mathf.Clamp(healthPercentage, 0f, 1f);

        healthText.text = (healthPercentage * 100) + "%";
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(healthPercentage, 1, 1);

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
        else if (currentHealth <= 25)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else if (currentHealth <= 50)
        {
            gameObject.GetComponent<Image>().color = RGB(255, 175, 0);
        }
        else
        {
            gameObject.GetComponent<Image>().color = RGB(140, 255, 113);
        }
    }
}
