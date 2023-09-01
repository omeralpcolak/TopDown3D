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

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, shootingDirection, out hit, enemyBulletSpeed * Time.deltaTime))
        {
            // Check if the ray hit something.
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                // Destroy the bullet when it hits a wall.
                DestroyBullet();
            }
        }
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
    }

  
    private void DestroyBullet()
    {
        Destroy(gameObject);
        Instantiate(enemyBulletHitEffect, transform.position, Quaternion.identity);
    }
    
}
