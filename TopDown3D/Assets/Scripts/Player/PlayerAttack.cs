using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject instantiateEffect;

    public Transform spawnPoint;
    public float bulletCooldown;

    float lastShotTime;

    private void Start()
    {
        lastShotTime = -bulletCooldown;
    }


    public void SpawnBullet()
    {
        if (Time.time - lastShotTime >= bulletCooldown)
        {
            instantiateEffect.gameObject.SetActive(false);
            SpawnBulletAndEffect();
            lastShotTime = Time.time;
        }
    }

    private void  SpawnBulletAndEffect()
    {
        instantiateEffect.gameObject.SetActive(true);
        Vector3 playerFacingDirection = transform.forward;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(playerFacingDirection));
        
    }
}