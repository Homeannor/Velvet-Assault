using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    float speedX, speedY;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameObject hitbox;
    private GameObject player;
    private HealthHandling healthScript;
    
    // START //
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitbox = GameObject.FindGameObjectWithTag("Hitbox");
        healthScript = GameObject.Find("Bar").GetComponent<HealthHandling>();
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private float lastHitboxTime = 0f;
    public float cooldownTime = 0.1f;

    public void SpawnHitbox()
    {
        // if (Time.time - lastHitboxTime < cooldownTime) return;

        if (player != null && hitbox != null)
        {
            lastHitboxTime = Time.time;

            GameObject hitbox = GameObject.FindGameObjectWithTag("Hitbox");
            GameObject hitboxClone = Instantiate(hitbox, player.transform.position, Quaternion.identity);

            bool isFlipped = player.GetComponent<SpriteRenderer>().flipX;
            hitboxClone.transform.position += new Vector3(isFlipped ? -2 : 2, 0, 0);
            hitboxClone.GetComponent<SpriteRenderer>().flipX = isFlipped;
            hitboxClone.SetActive(true);

            Destroy(hitboxClone, 0.3f);
        }
    }


    // UPDATE //
    void Update()
    {
        speedX = Input.GetAxis("Horizontal") * speed;
        speedY = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector2(speedX, speedY);

        // Flip the player sprite
        if (speedX > 0)
        {
            sr.flipX = false;
        }
        else if (speedX < 0)
        {
            sr.flipX = true;
        }

        // Keyboard inputs
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnHitbox();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentHealth > 0)
            {
                currentHealth -= 10f;
                healthScript.updateHealth();
            }
            else
            {
                currentHealth = 10f;
                healthScript.updateHealth();
            }
        }
    }
}