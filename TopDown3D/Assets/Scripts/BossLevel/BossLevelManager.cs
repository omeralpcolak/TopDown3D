using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //staircase.transform.DOMoveY(0f, 1f);
            staircase.transform.DOScale(new Vector3(10, 1, 2.5f), 1f);
            yield return new WaitForSeconds(.3f);
        }

        yield return new WaitForSeconds(0.8f);
        bossGround.transform.DOScale(1f, 0.8f).OnComplete(delegate
        {
            boss.transform.DOMoveY(5f, 2f).OnComplete(delegate
            {
                ReverseMovement();
                Instantiate(bossDropEffect, bossDropEffectPos.position, Quaternion.identity);
                bossController.BossAnim();
                CameraShake.instance.ShakeCamera(3f);
            });
        });
    }

    IEnumerator StaircaseObjectsMovementRtn()
    {
        yield return new WaitForSeconds(6f);

        foreach (GameObject staircaseObject in staircaseObjects)
        {
            staircaseObject.transform.DOMoveY(0f, 1f);
            //staircaseObject.transform.DOScale(1f,1f);
            yield return new WaitForSeconds(.3f);
        }
    }

    private void ReverseMovement()
    {
        StartCoroutine(ReverseStaircaseMovement());
        StartCoroutine(ReverseObjectMovement());
    }

    IEnumerator ReverseStaircaseMovement()
    {
        List<GameObject> reverseStaircases = new List<GameObject>();
        reverseStaircases = new List<GameObject>(staircases);
        reverseStaircases.Reverse();

        foreach(GameObject reverseStaircase in reverseStaircases)
        {
            reverseStaircase.transform.DOScale(0, 1f);
            //reverseStaircase.transform.DOMoveY(-40f, 2f);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator ReverseObjectMovement()
    {
        List<GameObject> reverseObjects = new List<GameObject>();
        reverseObjects = new List<GameObject>(staircaseObjects);
        reverseObjects.Reverse();

        foreach (GameObject reverseObject in reverseObjects)
        {
            reverseObject.transform.DOMoveY(35f, 2f).OnComplete(delegate
            {
                //reverseObject.gameObject.SetActive(false);
            });
            yield return new WaitForSeconds(0.3f);
        }
    }

}
