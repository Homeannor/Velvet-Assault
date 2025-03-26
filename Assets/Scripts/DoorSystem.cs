using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public float enemiesLeft = 3f;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI roomText;
    public GameObject player;
    public GameObject healthBar;
    public GameObject enemiesFolder;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        roomText.text = player.GetComponent<PlayerMovement>().roomName;
        clearText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enemiesLeft <= 0)
        {
            clearText.gameObject.SetActive(true);
            StartCoroutine(ClearText(1.5f));
        }
    }

    IEnumerator ClearText(float timeTaken)
    {
        yield return new WaitForSeconds(timeTaken);
        clearText.gameObject.SetActive(false);

        if (player.GetComponent<PlayerMovement>().roomName == "[1] Shop Entrance")
        {
            player.GetComponent<PlayerMovement>().roomName = "[2] The Kitchen";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[2] The Kitchen")
        {
            player.GetComponent<PlayerMovement>().roomName = "[3] Back Hallway";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[3] Back Hallway")
        {
            player.GetComponent<PlayerMovement>().roomName = "[4] Managers' Office";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[4] Managers' Office")
        {
            player.GetComponent<PlayerMovement>().roomName = "[5] Enter The Factory";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[5] Enter The Factory")
        {
            player.GetComponent<PlayerMovement>().roomName = "[6] Mainline Chaos";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[6] Mainline Chaos")
        {
            player.GetComponent<PlayerMovement>().roomName = "[7] THE SWARM.";
        }
        else if (player.GetComponent<PlayerMovement>().roomName == "[7] THE SWARM.")
        {
            player.GetComponent<PlayerMovement>().roomName = "[BOSS] ... Cake Day!?";
        }

        enemiesFolder.transform.Find(player.GetComponent<PlayerMovement>().roomName).gameObject.SetActive(true);

        roomText.text = player.GetComponent<PlayerMovement>().roomName;
        player.GetComponent<PlayerMovement>().currentHealth = Mathf.Clamp(player.GetComponent<PlayerMovement>().currentHealth + 25f, 0f, player.GetComponent<PlayerMovement>().maxHealth);
        healthBar.GetComponent<HealthHandling>().updateHealth();

        gameObject.SetActive(false);
    }
}
