using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossLevelManager : MonoBehaviour
{
    public List<GameObject> staircases = new List<GameObject>();
    public List<GameObject> staircaseObjects = new List<GameObject>();

    public GameObject bossGround;
    public GameObject boss;
    public GameObject bossDropEffect;

    public Transform bossDropEffectPos;

    BossController bossController;

    private void Awake()
    {
        bossController = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        BeginningBossLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BeginningBossLevel()
    {
        StartCoroutine(StaircaseMovementRtn());
        StartCoroutine(StaircaseObjectsMovementRtn());
        
    }

    IEnumerator StaircaseMovementRtn()
    {
        yield return new WaitForSeconds(6f);
        foreach (GameObject staircase in staircases)
        {
            staircase.transform.DOMoveY(0f, 1f);
            yield return new WaitForSeconds(.3f);
        }

        yield return new WaitForSeconds(0.8f);
        bossGround.transform.DOScale(1f, 0.8f).OnComplete(delegate
        {
            boss.transform.DOMoveY(5f, 2f).OnComplete(delegate
            {
                Instantiate(bossDropEffect, bossDropEffectPos.position, Quaternion.identity);
                bossController.BossAnim();
            });
        });
    }

    IEnumerator StaircaseObjectsMovementRtn()
    {
        yield return new WaitForSeconds(6f);

        foreach (GameObject staircaseObject in staircaseObjects)
        {
            staircaseObject.transform.DOMoveY(0f, 1f);
            yield return new WaitForSeconds(.3f);
        }
    }

}
