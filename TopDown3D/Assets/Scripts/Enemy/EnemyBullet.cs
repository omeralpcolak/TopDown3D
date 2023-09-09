using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float enemyBulletSpeed;
    public int enemyBulletDamageAmount;

    public GameObject enemyBulletHitEffect;

    Vector3 shootingDirection;


    PlayerHealthController playerHealthController;

    private void Start()
    {
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
    }


    private void FixedUpdate()
    {
        transform.Translate(shootingDirection * enemyBulletSpeed * Time.deltaTime);
    }


    public void SetShootingDirection(Vector3 direction)
    {
        shootingDirection = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            playerHealthController.PlayerTakeDamage(enemyBulletDamageAmount);
            DestroyBullet();
        }
        if (other.tag == "Wall")
        {
            DestroyBullet();
        }
    }

  
    private void DestroyBullet()
    {
        Destroy(gameObject);
        Instantiate(enemyBulletHitEffect, transform.position, Quaternion.identity);
    }
    
}
