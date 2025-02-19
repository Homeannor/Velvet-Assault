using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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

    // Health & Score Variables
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    private ScoreHandling scoreScript;
    public GameObject hitbox;

    // START //
    void Start()
    {
        hitbox = GameObject.FindGameObjectWithTag("EnemyHitbox");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        scoreScript = GameObject.Find("Score").GetComponent<ScoreHandling>();
        shouldFollow = true;
        randomOffset = Random.Range(-1f, 1f);
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

            Destroy(hitboxClone, 0.3f);
        }
    }

    // UPDATE //
    void Update()
    {
        // Health Check
        if (currentHealth <= 0)
        {
            scoreScript.updateScore(scoreReward);
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
