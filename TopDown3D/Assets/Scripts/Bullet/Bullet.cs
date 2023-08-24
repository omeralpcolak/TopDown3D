using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamageAmount;
    public GameObject hitEffect, wallHitEffect;
    public List<GameObject> bulletHitEffects = new List<GameObject>();

    void FixedUpdate()
    {

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, transform.rotation);
            CameraShake.instance.ShakeCamera(1.5f);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, transform.rotation);
            RandomHitEffectSpawn();
            CameraShake.instance.ShakeCamera(1.5f);
            collision.gameObject.GetComponent<EnemyHealthController>().EnemyTakeDamage(bulletDamageAmount);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Instantiate(wallHitEffect, transform.position, transform.rotation);
        }
    }

    private void RandomHitEffectSpawn()
    {
        int randomIndex = Random.Range(0, bulletHitEffects.Count);
        GameObject randomHitEffect = bulletHitEffects[randomIndex];
        Vector3 hitPos = transform.position;
        float randomYoffset = Random.Range(1, 3);
        hitPos.y += randomYoffset;
        Instantiate(randomHitEffect, hitPos, Quaternion.identity);

    }
}
