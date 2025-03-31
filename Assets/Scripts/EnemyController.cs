using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    // Enemy Following Variables
    public Transform player;
    public float followRange = 8f;
    public float moveSpeed = 3f;
    public float followChance = 1f;
    public float stopDistance = 3f;
    public float attackRange = 3f;
    public float attackDamage = 2f;
    public float scoreReward = 10f;
    public float randomOffset;
    private bool shouldFollow = false;
    public float strength = 10f;

    // Health & Score Variables
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    private ScoreHandling scoreScript;
    private HealthHandling healthScript;
    public GameObject hitbox;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI finalTimerText;

    // START //
    void Start()
    {
        hitbox = GameObject.FindGameObjectWithTag("EnemyHitbox");
        healthScript = GameObject.Find("Bar").GetComponent<HealthHandling>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        scoreScript = GameObject.Find("Canvas").GetComponent<ScoreHandling>();
        shouldFollow = true;
        // randomOffset = Random.Range(-1f, 1f);
    }

    private float lastHitboxTime;
    public float cooldownTime = 10f;

    public void SpawnHitbox()
    {
        if (player != null && hitbox != null)
        {
            lastHitboxTime = Time.time;
            Debug.Log("Last Hitbox Time: " + lastHitboxTime);

            GameObject hitbox = GameObject.FindGameObjectWithTag("EnemyHitbox");
            GameObject hitboxClone = Instantiate(hitbox, gameObject.transform.position, Quaternion.identity);
            hitboxClone.GetComponent<HitboxCollision>().damage = attackDamage;

            bool isFlipped = gameObject.GetComponent<SpriteRenderer>().flipX;
            hitboxClone.transform.position += new Vector3(isFlipped ? -2 : 2, 0, 0);
            hitboxClone.GetComponent<SpriteRenderer>().flipX = isFlipped;
            hitboxClone.SetActive(true);
            hitboxClone.GetComponent<HitboxCollision>().hitboxCreator = gameObject;
            hitboxClone.GetComponent<HitboxCollision>().knockbackForce = strength;

            Destroy(hitboxClone, 0.3f);
        }
    }

    // UPDATE //
    void Update()
    {
        // Health Check
        if (currentHealth <= 0f)
        {
            scoreScript.updateScore(scoreReward);
            GameObject.Find("Player").GetComponent<PlayerMovement>().currentHealth += 6f;
            GameObject.Find("Bar").GetComponent<HealthHandling>().updateHealth();
            // Debug.Log("Healed Player by 3");

            GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");
            foreach (GameObject door in doors)
            {
                door.GetComponent<DoorSystem>().enemiesLeft -= 1f;
            }

            if (gameObject.name == "Cake Day")
            {
                TimeSpan finalTime = TimeSpan.FromSeconds(scoreScript.timer);
                PlayerPrefs.SetString("FinalTime", finalTime.Minutes.ToString("00") + ":" + finalTime.Seconds.ToString("00") + ":" + finalTime.Milliseconds.ToString("000"));
                
                if (scoreScript.timer < 360f)
                {
                    PlayerPrefs.SetString("FinalRank", "S");
                }
                else if (scoreScript.timer < 420f)
                {
                    PlayerPrefs.SetString("FinalRank", "A");
                }
                else if (scoreScript.timer < 480f)
                {
                    PlayerPrefs.SetString("FinalRank", "B");
                }
                else if (scoreScript.timer < 540f)
                {
                    PlayerPrefs.SetString("FinalRank", "C");
                }
                else if (scoreScript.timer < 600f)
                {
                    PlayerPrefs.SetString("FinalRank", "D");
                }
                else
                {
                    PlayerPrefs.SetString("FinalRank", "F");
                }
                
                SceneManager.LoadScene(3);
            }

            Destroy(gameObject);
        }

        // Enemy Following
        if (!shouldFollow) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        // Debug.Log("Distance to player: " + distanceToPlayer);

        if (distanceToPlayer <= followRange)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX = player.position.x < transform.position.x;

            if (distanceToPlayer <= attackRange && Time.time >= lastHitboxTime + cooldownTime)
            {
                SpawnHitbox();
            }
        }
    }
}
