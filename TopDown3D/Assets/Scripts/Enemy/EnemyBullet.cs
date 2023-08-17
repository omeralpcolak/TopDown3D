using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float enemyBulletSpeed;
    Vector3 shootingDirection;


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
        }
    }
}
