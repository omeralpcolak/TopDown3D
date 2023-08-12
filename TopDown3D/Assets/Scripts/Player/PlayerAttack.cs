using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    

    public void SpawnBullet()
    {
        Vector3 playerFacingDirection = transform.forward;
        Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(playerFacingDirection));
    }
}
