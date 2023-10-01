using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public float playerCurrentHealth, playerMaxHealth;

    [SerializeField]Healthbar healthbar;

    [SerializeField] GameObject deathEffect;

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
    }


    public void PlayerTakeDamage(int damageAmount)
    {
        playerCurrentHealth -= damageAmount;
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
        if (playerCurrentHealth <= 0)
        {
            GameManager.instance.GameOver();
            playerCurrentHealth = 0f;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    public void GainHealth(int healthAmount)
    {
        playerCurrentHealth += healthAmount;
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
    }

}
