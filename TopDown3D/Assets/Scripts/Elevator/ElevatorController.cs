using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElevatorController : MonoBehaviour
{
    public Transform targetPosition;
    public GameObject player;
    //public GameObject lightSticks;
    [SerializeField] float moveDuration;
    [SerializeField] float rotationAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        /*lightSticks.transform.DORotate(new Vector3(0f, rotationAngle, 0f), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);*/
            


        transform.DOMove(targetPosition.position, moveDuration).OnComplete(delegate
        {
            player.transform.parent = null;   
            transform.DOScale(0f, 1.5f).OnComplete(delegate
            {
                player.GetComponent<PlayerMovement>().JoyStickActivasionFunc();
                Destroy(gameObject);
                
            });

        });

        

        
    }

    
}
