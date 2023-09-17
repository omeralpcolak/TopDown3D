using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackSpeed : Powerup
{
    public GameObject attackSpeedTxt;


    private void Start()
    {
        PowerUpAnim(360f, 0.6f);
        powerupName = "AttackSpeed";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHealthController != null)
            {
                ApplyPowerup(powerupName);
                PowerUpTxtEffect(attackSpeedTxt);
                powerupController.PowerupPickedUp();
            }

            transform.DOScale(0, .8f).OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
        }
    }







}
