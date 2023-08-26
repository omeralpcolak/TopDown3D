using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth;
    public GameObject enemyDeathEffect;
    public GameObject xp;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void EnemyTakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        currentHealth = 0;
        Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Instantiate(xp, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
