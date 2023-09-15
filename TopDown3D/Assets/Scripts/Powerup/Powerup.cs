using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Powerup : MonoBehaviour
{

    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAttack playerAttack;
    [HideInInspector] public PlayerHealthController playerHealthController;
    [HideInInspector] public PowerupController powerupController;

    [HideInInspector] public string powerupName;

    private int healthAmount = 10;

    private float speedMultiplier = 1.2f;
    private float attackSpeedMultiplier = 1.2f;


    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        powerupController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PowerupController>();
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


    public void PowerUpTxtEffect(GameObject txtEffect)
    {
        StartCoroutine(PowerupTxtEffectRtn(txtEffect));
    }

    IEnumerator PowerupTxtEffectRtn(GameObject effect)
    {
        effect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        effect.gameObject.SetActive(false);
    }


    public void PowerUpAnim(float rotation,float moveUpwards)
    {
        transform.DORotate(new Vector3(0f, rotation, 0f), 1f, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        Vector3 moveUpwardsDir = transform.position + new Vector3(0f, moveUpwards, 0f);

        transform.DOMove(moveUpwardsDir, 1f)
        .SetLoops(-1, LoopType.Yoyo);
    }

}
