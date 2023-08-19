using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnBox : MonoBehaviour
{
    public GameObject topWall,leftWall,rightWall,bottomWall,backWall,frontWall;
    public GameObject enemySpawnBoxEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnBoxRtn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator EnemySpawnBoxRtn()
    {
        transform.DOMoveY(-4.81f, 1f).OnComplete(delegate
        {
            CameraShake.instance.ShakeCamera(3f);
            EnemySpawnBoxEffectPos();
            topWall.transform.DOMoveY(10f, 1.5f).OnComplete(delegate
            {
                topWall.transform.DOScale(0f, 1f);
            });

            bottomWall.transform.DOScale(0f, 1.5f);

            leftWall.transform.DOMoveX(-10f, 1.5f).OnComplete(delegate
            {
                leftWall.transform.DOScale(0f, 1f);
            });

            rightWall.transform.DOMoveX(10f, 1.5f).OnComplete(delegate
            {
                rightWall.transform.DOScale(0f, 1f);
            });

            frontWall.transform.DOMoveZ(-10f, 1.5f).OnComplete(delegate
            {
                frontWall.transform.DOScale(0f, 1f);
            });

            backWall.transform.DOMoveZ(10f, 1.5f).OnComplete(delegate
            {
                backWall.transform.DOScale(0f, 1f);
            });



        });


        yield return null;
    }


    private void EnemySpawnBoxEffectPos()
    {
        Vector3 effectPos = transform.position;
        effectPos.y += 4f;
        Instantiate(enemySpawnBoxEffect, effectPos, Quaternion.identity);

    }

}
