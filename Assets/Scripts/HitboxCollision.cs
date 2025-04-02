using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxCollision : MonoBehaviour
{
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.3f;
    public float damage = 10f;
    public GameObject hitboxCreator;
    public GameObject minibarClone;

    private HealthHandling healthScript;

    void Start()
    {
        healthScript = GameObject.Find("Bar").GetComponent<HealthHandling>();
        minibarClone = GameObject.Find("Minibar");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Hitbox") && other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            Rigidbody2D creatorRb = hitboxCreator.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                // Enemy Knockback
                Vector2 knockbackDirection = (new Vector2(creatorRb.position.x, transform.position.y) + new Vector2(transform.position.x, transform.position.y)).normalized;
                knockbackDirection.y = Mathf.Clamp(knockbackDirection.y, -0.5f, 0.5f);

                // Vector2 knockbackTargetPosition = creatorRb.position + knockbackDirection * knockbackForce;
                StartCoroutine(SmoothKnockback(enemyRb, knockbackDirection, knockbackForce));

                // Enemy Damage
                if (other.GetComponent<EnemyController>().currentHealth > 0f)
                {
                    other.GetComponent<EnemyController>().currentHealth -= damage;
                    Debug.Log("Dealt " + damage + " damage to the enemy");

                    // Spawn Minibar
                    if (!other.transform.Find("Minibar(Clone)"))
                    {
                        GameObject newMinibar = Instantiate(minibarClone, other.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
                        newMinibar.transform.SetParent(other.transform);
                        newMinibar.GetComponent<MinibarHandling>().barOwner = other.gameObject;
                        newMinibar.GetComponent<MinibarHandling>().updateBar();
                    }
                    else
                    {
                        other.transform.Find("Minibar(Clone)").GetComponent<MinibarHandling>().updateBar();
                    }
                }
            }
        }
        else if (gameObject.CompareTag("EnemyHitbox") && other.CompareTag("Player"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            Rigidbody2D creatorRb = hitboxCreator.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                // Enemy Knockback
                Vector2 knockbackDirection = (new Vector2(creatorRb.position.x, transform.position.y) + new Vector2(transform.position.x, transform.position.y)).normalized;
                knockbackDirection.y = Mathf.Clamp(knockbackDirection.y, -0.5f, 0.5f);

                // Vector2 knockbackTargetPosition = creatorRb.position + knockbackDirection * knockbackForce;
                StartCoroutine(SmoothKnockback(enemyRb, -knockbackDirection, knockbackForce));

                // Player Damage
                if (other.GetComponent<PlayerMovement>().currentHealth > 0f)
                {
                    other.GetComponent<PlayerMovement>().currentHealth -= damage;
                    healthScript.updateHealth();
                    Debug.Log("Dealt " + damage + " damage to the player");
                }
            }
        }
    }

    /*private IEnumerator SmoothKnockback(Rigidbody2D enemyRb, Vector2 targetPosition)
    {
        Vector2 startPosition = enemyRb.position;
        float timeElapsed = 0f;

        while (timeElapsed < knockbackDuration)
        {
            enemyRb.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / knockbackDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        enemyRb.position = targetPosition;
    }*/

    private IEnumerator SmoothKnockback(Rigidbody2D enemyRb, Vector2 knockbackDirection, float knockbackForce)
    {
        float timeElapsed = 0f;
        
        while (timeElapsed < knockbackDuration)
        {
            enemyRb.linearVelocity = knockbackDirection * knockbackForce * (1f - (timeElapsed / knockbackDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        enemyRb.linearVelocity = Vector2.zero;
    }

}
