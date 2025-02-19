using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandling : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth()
    {
        float currentHealth = player.GetComponent<PlayerMovement>().currentHealth;
        float maxHealth = player.GetComponent<PlayerMovement>().maxHealth;
        float healthPercentage = (currentHealth / maxHealth);
        healthPercentage = Mathf.Clamp(healthPercentage, 0f, 1f);
        
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(healthPercentage, 1, 1);
    }
}
