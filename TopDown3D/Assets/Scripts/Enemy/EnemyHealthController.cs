using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] float currentHealth, maxHealth;
    public GameObject enemyDeathEffect;
    LevelXpController levelXpController;
    
    private void Start()
    {
        currentHealth = maxHealth;
        levelXpController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelXpController>();
    }



    public void EnemyTakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            GameManager.instance.GetComponent<ShopManager>().GainSoul();
            StartCoroutine(EnemyDeathRtn());
        }
    }


    public void EnemyDeathAtGameOver()
    {
        currentHealth = 0;
        Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    

    IEnumerator EnemyDeathRtn()
    {
        currentHealth = 0;
        Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        levelXpController.SpawnXp(transform);
        levelXpController.canXpInstan = false;
        Destroy(gameObject);
        levelXpController.canXpInstan = true;
        yield return null;
    }
}
