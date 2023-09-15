using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerupController : MonoBehaviour
{
    public bool canTeleport;
    public bool powerupPickedUp = false;
    public bool hasTeleported;

    public float _currentLevel;

    LevelXpController levelXpController;

    public List<GameObject> powerups = new List<GameObject>();

    Transform player;
    [SerializeField] Transform teleportationPos, teleportBackPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        levelXpController = GetComponent<LevelXpController>();
    }


    private void Update()
    {
 

        if (canTeleport && !hasTeleported)
        {
            StartCoroutine(Teleport());
            hasTeleported = true;
        }

        if (powerupPickedUp)
        {
            StartCoroutine(TeleportBack());
            powerupPickedUp = false;
            canTeleport = false;

        }

    }

    private IEnumerator Teleport()
    {
        player.position = teleportationPos.position;
        yield return new WaitForSeconds(2f);
        ActivatePowerUps();
    }

    private IEnumerator TeleportBack()
    {
        DeactivateOthersPowerUps();
        yield return new WaitForSeconds(2f);
        player.position = teleportBackPos.position;
        hasTeleported = false;
    }

    private void DeactivateOthersPowerUps()
    {
        foreach (GameObject powerup in powerups)
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