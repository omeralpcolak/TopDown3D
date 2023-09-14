using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerupController : MonoBehaviour
{
    public bool canTeleport;
    public bool canTeleportBack;
    public bool powerupPickedUp = false;
    private bool hasTeleported=false;
    

    public List<GameObject> powerups = new List<GameObject>();

    Transform player;
    [SerializeField] Transform teleportationPos,teleportBackPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    private void Update()
    {
        if(canTeleport && !hasTeleported)
        {
            Teleport();
            ActivatePowerUps();
            hasTeleported = true;
        }

        if (powerupPickedUp)
        {
            DeactivateOthersPowerUps();
            powerupPickedUp = false;
            TeleportBack();
        }
        
    }

    private void Teleport()
    {
        player.position = teleportationPos.position;
    }

    private void TeleportBack()
    {
        player.position = teleportBackPos.position;
        
    }

    private void DeactivateOthersPowerUps()
    {
        foreach(GameObject powerup in powerups)
        {
            powerup.transform.DOScale(0f, 1f).OnComplete(delegate
            {
                powerup.gameObject.SetActive(false);
            });
        }
    }

    private void ActivatePowerUps()
    {
        foreach (GameObject powerup in powerups)
        {
            powerup.gameObject.SetActive(true);
            powerup.transform.DOScale(1, 1f);
        }
    }

    public void PowerUpPickedUp()
    {
        powerupPickedUp = true;
    }
}
