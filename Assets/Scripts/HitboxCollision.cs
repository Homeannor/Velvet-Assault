using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCollision : MonoBehaviour
{
    public float knockbackForce = 1f;
    public float knockbackDuration = 0.3f;
    public float damage = 10f;

    private HealthHandling healthScript;

    void Start()
    {
        healthScript = GameObject.Find("Bar").GetComponent<HealthHandling>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Hitbox") && other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                // Enemy Knockback
               Vector2 knockbackDirection = (new Vector2(other.transform.position.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
                knockbackDirection.y = Mathf.Clamp(knockbackDirection.y, -0.5f, 0.5f);

                Vector2 knockbackTargetPosition = enemyRb.position + knockbackDirection * knockbackForce;
                StartCoroutine(SmoothKnockback(enemyRb, knockbackTargetPosition));

                // Enemy Damage
                if (other.GetComponent<EnemyController>().currentHealth > 0f)
                {
                    other.GetComponent<EnemyController>().currentHealth -= damage;
                    Debug.Log("Dealt 10 damage to the enemy");
                }
            }
        }
        else if (gameObject.CompareTag("EnemyHitbox") && other.CompareTag("Player"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                // Enemy Knockback
               Vector2 knockbackDirection = (new Vector2(other.transform.position.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
                knockbackDirection.y = Mathf.Clamp(knockbackDirection.y, -0.5f, 0.5f);

                Vector2 knockbackTargetPosition = enemyRb.position + knockbackDirection * knockbackForce;
                StartCoroutine(SmoothKnockback(enemyRb, knockbackTargetPosition));

                // Enemy Damage
                if (other.GetComponent<PlayerMovement>().currentHealth > 0f)
                {
                    other.GetComponent<PlayerMovement>().currentHealth -= damage;
                    healthScript.updateHealth();
                    Debug.Log("Dealt 10 damage to the player");
                }
            }
        }
    }

    private IEnumerator SmoothKnockback(Rigidbody2D enemyRb, Vector2 targetPosition)
    {
        Vector2 startPosition = enemyRb.position;
        float timeElapsed = 0f;

        while (timeElapsed < knockbackDuration)
        {
            // Lerp for smooth knockback
            enemyRb.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / knockbackDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        enemyRb.position = targetPosition;
    }
}
