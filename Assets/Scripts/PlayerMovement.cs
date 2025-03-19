using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 2f;
    private bool dashing = false;
    private bool canDash = true;
    private float lastDash;
    float dashCooldownStart;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.3f;
    float speedX, speedY;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public GameObject hitbox;
    private GameObject player;
    private HealthHandling healthScript;
    public Camera MainCamera;
    
    // START //
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hitbox = GameObject.FindGameObjectWithTag("Hitbox");
        healthScript = GameObject.Find("Bar").GetComponent<HealthHandling>();
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        healthScript.updateHealth();
    }

    // UPDATE //
    void Update()
    {
        MovementLogic();
        Dash();

        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnHitbox();
        }

        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentHealth = Mathf.Max(0, currentHealth - 10f);
            healthScript.updateHealth();
        }*/
    }

    public void MovementLogic()
    {
        speedX = Input.GetAxis("Horizontal") * speed;
        speedY = Input.GetAxis("Vertical") * speed;
        rb.velocity = new Vector2(speedX, speedY);

        // Flip the player sprite
        sr.flipX = speedX < 0;
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing && canDash)
        {
            dashing = true;
            canDash = false;
            lastDash = Time.time;
            speed = dashSpeed;
            dashCooldownStart = Time.time;

            player.transform.Find("Dash Particles").GetComponent<ParticleSystem>().Play();
            MainCamera.GetComponent<CameraFollow>().moveLerpSpeed = 10f;
        }

        if (dashing && Time.time >= lastDash + dashDuration)
        {
            speed = 5f;
            dashing = false;

            player.transform.Find("Dash Particles").GetComponent<ParticleSystem>().Stop();
            MainCamera.GetComponent<CameraFollow>().moveLerpSpeed = 2f;
        }

        if (!canDash && Time.time >= dashCooldownStart + dashCooldown)
        {
            canDash = true;
        }
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
            hitboxClone.GetComponent<HitboxCollision>().hitboxCreator = player;

            Destroy(hitboxClone, 0.3f);
        }
    }

    private void onCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.gameObject.GetComponent<Rigidbody2D>();
            enemyRb.velocity = Vector2.zero;
        }
    }
}