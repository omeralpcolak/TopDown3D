using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerHealthController playerHealthController;

    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
    }

    public virtual void ApplyPowerup()
    {

    }

}
