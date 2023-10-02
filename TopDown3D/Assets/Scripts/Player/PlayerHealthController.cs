using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthController : MonoBehaviour
{
    public float playerCurrentHealth, playerMaxHealth;

    public GameObject takeDamageUI;

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
        StartCoroutine(TakeDamageUIRtn(0.1f));
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
        if (playerCurrentHealth <= 0)
        {
            GameManager.instance.GameOver();
            takeDamageUI.GetComponent<CanvasGroup>().DOFade(0f, 0.1f);
            playerCurrentHealth = 0f;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    IEnumerator TakeDamageUIRtn(float uiDuration)
    {
        takeDamageUI.GetComponent<CanvasGroup>().DOFade(0.05f, 0.1f);
        yield return new WaitForSeconds(uiDuration);
        takeDamageUI.GetComponent<CanvasGroup>().DOFade(0f, 0.1f);

    }

    public void GainHealth(int healthAmount)
    {
        playerCurrentHealth += healthAmount;
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
    }

}
