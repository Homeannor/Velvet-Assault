using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MinibarHandling : MonoBehaviour
{
    public GameObject barOwner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    Color RGB(int r, int g, int b, float a = 1f)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a);
    }

    public void updateBar()
    {
        float currentHealth = barOwner.GetComponent<EnemyController>().currentHealth;
        float maxHealth = barOwner.GetComponent<EnemyController>().maxHealth;
        float healthPercentage = (currentHealth / maxHealth);
        healthPercentage = Mathf.Clamp(healthPercentage, 0f, 1f);
        
        gameObject.transform.localScale = new Vector3(healthPercentage, 0.125f, 1);

        if (currentHealth <= 25)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (currentHealth <= 50)
        {
            gameObject.GetComponent<SpriteRenderer>().color = RGB(255, 175, 0);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = RGB(140, 255, 113);
        }
    }
}
