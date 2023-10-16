using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private int laserDamage = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject != null)
            {
                PlayerHealthController playerHealthController = other.gameObject.GetComponent<PlayerHealthController>();
                playerHealthController.PlayerTakeDamage(laserDamage);
            }
        }
    }
}
