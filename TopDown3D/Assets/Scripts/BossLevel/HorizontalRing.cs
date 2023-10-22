using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRing : MonoBehaviour
{
    [SerializeField] int ringDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController playerHealthController = other.gameObject.GetComponent<PlayerHealthController>();
            playerHealthController.PlayerTakeDamage(ringDamage);
        }
    }
}
