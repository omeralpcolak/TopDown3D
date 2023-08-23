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
    public float wallMovAmount, wallMovDuration;
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

            Vector3 topWallMove = topWall.transform.position + new Vector3(0, wallMovAmount, 0);

            topWall.transform.DOMove(topWallMove, wallMovDuration).OnComplete(delegate
            {
                topWall.transform.DOScale(0f, 1f);
            });

            bottomWall.transform.DOScale(0f, 1f);

            Vector3 leftWallMove = leftWall.transform.position + new Vector3(-wallMovAmount, 0, 0);

            leftWall.transform.DOMove(leftWallMove, wallMovDuration).OnComplete(delegate
            {
                leftWall.transform.DOScale(0f, 1f);
            });

            Vector3 rightWallMove = rightWall.transform.position + new Vector3(wallMovAmount, 0, 0);

            rightWall.transform.DOMove(rightWallMove, wallMovDuration).OnComplete(delegate
            {
                rightWall.transform.DOScale(0f, 1f);
            });

            Vector3 frontWallMove = frontWall.transform.position + new Vector3(0, 0, -wallMovAmount); 

            frontWall.transform.DOMove(frontWallMove, wallMovDuration).OnComplete(delegate
            {
                frontWall.transform.DOScale(0f, 1f);
            });

            Vector3 backWallMove = backWall.transform.position + new Vector3(0, 0, wallMovAmount);
            backWall.transform.DOMove(backWallMove, wallMovDuration).OnComplete(delegate
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
