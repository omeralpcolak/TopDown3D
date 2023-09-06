using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] float healthAmount = 10f;
    PlayerHealthController playerHealthController;

    public GameObject healthTxtEffect;

    private void Awake()
    {
        
    }

    private void Start()
    {
        playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
        HealthPickUpAnim();
    }

    private void Update()
    {
        if(playerHealthController == null)
        {
            transform.DOScale(0, 1f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(playerHealthController != null)
            {
                playerHealthController.GainHealth(healthAmount);
                StartCoroutine(HealthTxtEffect());
            }

            

            transform.DOScale(0, .8f).OnComplete(delegate
            {
                Destroy(gameObject);
            });
        }
    }



    private void HealthPickUpAnim()
    {
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
        .SetLoops(-1, LoopType.Restart)
        .SetRelative()
        .SetEase(Ease.Linear);

        Vector3 healthPickMove = transform.position + new Vector3(0f, 0.6f, 0f);

        transform.DOMove(healthPickMove, 1f)
        .SetLoops(-1, LoopType.Yoyo);
    }

    IEnumerator HealthTxtEffect()
    {
        healthTxtEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        healthTxtEffect.gameObject.SetActive(false);
    }
}
