using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthController : MonoBehaviour
{
    public float playerCurrentHealth, playerMaxHealth;

    public GameObject takeDamageUI;
    public GameObject bossHitEffect;

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
        CameraShake.instance.ShakeCamera(1f);
        DamageUIFade(0.4f);
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);

        if (playerCurrentHealth <= 0)
        {
            takeDamageUI.gameObject.SetActive(false);
            GameManager.instance.GameOver();
            playerCurrentHealth = 0f;
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    /*IEnumerator TakeDamageUIRtn(float uiDuration)
    {
        CanvasGroup fade = takeDamageUI.GetComponent<CanvasGroup>();
        fade.DOFade(0.4f, uiDuration).OnComplete(delegate
        {
            fade.DOFade(0, uiDuration);
        });
        yield return null;
        yield return new WaitForSeconds(uiDuration);
        takeDamageUI.GetComponent<CanvasGroup>().DOFade(0f, uiDuration);

    }*/

    public void BossHitEffect()
    {
        Instantiate(bossHitEffect, transform.position, Quaternion.identity);
    }

    private void DamageUIFade(float uiDuration)
    {
        CanvasGroup fade = takeDamageUI.GetComponent<CanvasGroup>();
        fade.DOFade(0.4f, uiDuration).OnComplete(delegate
        {
            fade.DOFade(0, uiDuration);
        });
    }

    public void GainHealth(int healthAmount)
    {
        playerCurrentHealth += healthAmount;
        healthbar.UpdateHealthBar(playerMaxHealth, playerCurrentHealth);
    }

}
