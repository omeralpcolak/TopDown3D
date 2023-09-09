using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform playerPos;
    float distanceToPlayer;
    public float enemyShootingRange;
    public float enemyBulletSpawnCd;
    public EnemyBullet enemyBulletPrefab;
    public Transform enemyBulletPos;

    bool canShoot = true;
    

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if(playerPos != null)
        {
            CalculateDistance();

            if (distanceToPlayer <= enemyShootingRange)
            {
                EnemyShooting();
            }
        }

        
    }

    private void CalculateDistance()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerPos.position);
    }

    private void EnemyShooting()
    {
        if (canShoot)
        {
            StartCoroutine(EnemyShootingRtn());
        }
        
    }

    IEnumerator EnemyShootingRtn()
    {
        canShoot = false;

        Vector3 shootingDirection = (playerPos.position - enemyBulletPos.position).normalized;
        EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab, enemyBulletPos.position, Quaternion.identity);
        enemyBullet.SetShootingDirection(shootingDirection);
        

        yield return new WaitForSeconds(enemyBulletSpawnCd);

        canShoot = true;
    }
}
