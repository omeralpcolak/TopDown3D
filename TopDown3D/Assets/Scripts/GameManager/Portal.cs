using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Portal : MonoBehaviour
{
    PowerupController powerupController;

    public GameObject portalEffect;

    private void Awake()
    {
        powerupController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PowerupController>();
    }
    void Start()
    {
        StartCoroutine(PortalAnim());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            powerupController.canTeleport = true;
            Destroy(gameObject);
        }    
    }


    

    IEnumerator PortalAnim()
    {
        transform.DOScale(100f, 1f).OnComplete(delegate
        {
            portalEffect.gameObject.SetActive(true);
        });
        yield return null;
    }

}
