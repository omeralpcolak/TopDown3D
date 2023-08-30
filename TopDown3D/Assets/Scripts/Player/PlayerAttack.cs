using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    [SerializeField] float bulletCooldown;

    float lastShotTime;

    private void Start()
    {
        lastShotTime = -bulletCooldown;
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
