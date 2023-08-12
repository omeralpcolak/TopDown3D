using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject hitEffect;

    void FixedUpdate()
    {

        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Instantiate(hitEffect, transform.position, transform.rotation);
            CameraShake.instance.ShakeCamera(1.5f, 0.1f);
        }
    }
}
