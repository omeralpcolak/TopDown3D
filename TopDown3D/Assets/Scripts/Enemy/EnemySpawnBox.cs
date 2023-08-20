using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnBox : MonoBehaviour
{
    public GameObject topWall,leftWall,rightWall,bottomWall,backWall,frontWall;
    public GameObject enemySpawnBoxEffect;
    public GameObject enemy;
    public Transform enemySpawnPos;
    public float wallMovDistance, wallMovDuration;
    public int enemySpawnNumber;

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
        EnemySpawnBoxAnim();


        yield return new WaitForSeconds(2f);

        //Instantiate(enemy, enemySpawnPos.position, Quaternion.identity);

        EnemySpawn(enemySpawnNumber);

        
    }

    private void EnemySpawn(int spawnCount)
    {
        for(int i = 0; i< enemySpawnNumber; i++)
        {
            Vector3 spawnOffset = new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
            Vector3 spawnPosition = enemySpawnPos.position + spawnOffset;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }

    private void EnemySpawnBoxAnim()
    {
        transform.DOMoveY(-4.81f, 1f).OnComplete(delegate
        {
            enemySpawnPos.parent = null;
            CameraShake.instance.ShakeCamera(3f);
            EnemySpawnBoxEffectPos();
            topWall.transform.DOMoveY(20f, wallMovDuration).OnComplete(delegate
            {
                topWall.transform.DOScale(0f, 1f);
            });

            bottomWall.transform.DOScale(0f, 1f);

            leftWall.transform.DOMoveX(-wallMovDistance, wallMovDuration).OnComplete(delegate
            {
                leftWall.transform.DOScale(0f, 1f);
            });

            rightWall.transform.DOMoveX(wallMovDistance, wallMovDuration).OnComplete(delegate
            {
                rightWall.transform.DOScale(0f, 1f);
            });

            frontWall.transform.DOMoveZ(-wallMovDistance, wallMovDuration).OnComplete(delegate
            {
                frontWall.transform.DOScale(0f, 1f);
            });

            backWall.transform.DOMoveZ(wallMovDistance, wallMovDuration).OnComplete(delegate
            {
                backWall.transform.DOScale(0f, 1f);
            });



        });
    }


    private void EnemySpawnBoxEffectPos()
    {
        Vector3 effectPos = transform.position;
        effectPos.y += 4f;
        Instantiate(enemySpawnBoxEffect, effectPos, Quaternion.identity);

    }

}
