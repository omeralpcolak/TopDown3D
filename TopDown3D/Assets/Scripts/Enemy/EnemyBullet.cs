using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed;
    Vector3 shootingDirection;
    public List<GameObject> enemyHitEffects = new List<GameObject>();

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
            Destroy(gameObject);
            RandomHitEffectSpawn();
            
        }
    }

    private void RandomHitEffectSpawn()
    {
        int randomIndex = Random.Range(0, enemyHitEffects.Count);
        GameObject randomHitEffect = enemyHitEffects[randomIndex];
        Instantiate(randomHitEffect, transform.position, Quaternion.identity);
        
    }
}
