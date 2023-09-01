using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Xp : MonoBehaviour
{
     PlayerMovement player;
    public float movementDuration;
    public float rotationDuration;
    LevelXpController levelXpController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        levelXpController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelXpController>();
        XpAnim();
    }

    private void Update()
    {
        if (player != null)
        {
            XpMovement();
        }
        else
        {
            DestroyGameobject();
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DestroyGameobject();
            levelXpController.GainXp();
            levelXpController.UpdateXpBar();
        }
    }

    public void XpAnim()
    {
        transform.DORotate(new Vector3(360f, 360f, 360f), rotationDuration, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);
    }

    private void XpMovement()
    {
        transform.DOMove(player.transform.position, movementDuration);
    }

    private void DestroyGameobject()
    {
        transform.DOScale(0f, 1f).OnComplete(delegate
        {
            Destroy(gameObject);
        });
    }
}
