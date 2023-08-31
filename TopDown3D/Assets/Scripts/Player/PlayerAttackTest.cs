using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTest : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform spawnPoint;

    private bool canAttack;
    [SerializeField] float bulletCooldown;

    float lastShotTime;

    private void Start()
    {
        lastShotTime = -bulletCooldown;
    }

    private void Update()
    {
        if (canAttack)
        {
            SpawnBullet();
        }
    }

    public void pointerDown()
    {
        canAttack = true;
    }

    public void pointerUp()
    {
        canAttack = false;
    }



    public void SpawnBullet()
    {
        if (Time.time - lastShotTime >= bulletCooldown)
        {
            StartCoroutine(SpawnBulletRtn());
            lastShotTime = Time.time;
        }
    }

    IEnumerator SpawnBulletRtn()
    {
        Vector3 playerFacingDirection = transform.forward;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(playerFacingDirection));
        yield return null;
    }

}
