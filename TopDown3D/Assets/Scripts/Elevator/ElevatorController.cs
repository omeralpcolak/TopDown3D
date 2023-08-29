using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorController : MonoBehaviour
{
    public Transform targetPosition;

    public GameObject player;
    public GameObject GameManager;

    public bool moveComplete = false;

    [SerializeField] float moveDuration;
    [SerializeField] float rotationAngle;

    
    


    public void ElevatorMove()
    {
        transform.DOMove(targetPosition.position, moveDuration).OnComplete(delegate
        {
            player.transform.parent = null;
            transform.DOScale(0f, 1.5f).OnComplete(delegate
            {
                gameObject.SetActive(false);
                

            });

        });

    }
    
}
