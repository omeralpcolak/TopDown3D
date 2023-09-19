using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerupController : MonoBehaviour
{
    public bool canTeleport;
    public bool powerupPickedUp = false;
    [HideInInspector]public bool hasTeleported;

    public event System.Action<bool> OnHasTeleportedChanged;

    public float _currentLevel;

    public List<GameObject> powerups = new List<GameObject>();

    Transform player;
    [SerializeField] Transform teleportationPos, teleportBackPos;
    Vector3 backPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {

        CheckingTeleportationConditions();
    }

    private void CheckingTeleportationConditions()
    {
        if (!canTeleport && !hasTeleported && player)
        {
            backPosition = player.position;
        }

        if (canTeleport && !hasTeleported)
        {
            StartCoroutine(Teleport());
        }

        if (powerupPickedUp)
        {
            StartCoroutine(TeleportBack());
        }
    }


    private IEnumerator Teleport()
    {
        player.position = teleportationPos.position;
        hasTeleported = true;
        OnHasTeleportedChanged?.Invoke(hasTeleported);
        yield return new WaitForSeconds(1.5f);
        ActivatePowerUps();
    }

    private IEnumerator TeleportBack()
    {
        DeactivateOthersPowerUps();
        yield return new WaitForSeconds(2f);
        player.position = backPosition;
        canTeleport = false;
        hasTeleported = false;
        powerupPickedUp = false;
        OnHasTeleportedChanged?.Invoke(hasTeleported);
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

    public void PowerupPickedUp()
    {
        powerupPickedUp = true;
    }
}
