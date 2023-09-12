using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerHealthController playerHealthController;

    [HideInInspector] public string powerupName;

    public int healthAmount;

    public float speedMultiplier;
    public float attackSpeedMultiplier;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
    }

    public void ApplyPowerup(string powerupName)
    {
        switch (powerupName)
        {
            case "Health":


                playerHealthController.GainHealth(healthAmount);
                

                break;

            case "AttackSpeed":

                playerAttack.bulletCooldown /= attackSpeedMultiplier;
                   

                break;

            case "MovementSpeed":


                playerMovement.movementSpeed *= speedMultiplier;


                break;
        }
    }

}
