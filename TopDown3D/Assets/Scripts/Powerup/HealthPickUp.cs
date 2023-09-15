using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthPickUp : Powerup
{
    

    public GameObject healthTxtEffect;

    

    private void Start()
    {
        PowerUpAnim(360f, 0.6f);
        powerupName = "Health";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(playerHealthController != null)
            {
                ApplyPowerup(powerupName);
                PowerUpTxtEffect(healthTxtEffect);
                powerupController.PowerUpPickedUp();
            }

            transform.DOScale(0, .8f).OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
        }
    }



    
}
