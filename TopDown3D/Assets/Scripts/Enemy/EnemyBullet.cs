using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed;
    public float enemyBulletDamageAmount;

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
            Destroy(gameObject);
            Instantiate(enemyBulletHitEffect, transform.position, Quaternion.identity);
            
        } 
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            RandomHitEffectSpawn();

        }
    }*/

    
}
