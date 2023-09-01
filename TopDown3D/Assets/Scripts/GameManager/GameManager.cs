using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject joySticks;
    public GameObject uiBar;
    public GameObject elevator;

    public float enemyBoxSpawnCd;

    public bool gameCanStart;

    EnemySpawnController enemySpawnController;


    private void Awake()
    {
        instance = this;
        enemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
        gameCanStart = true;
        StartCoroutine(GameStartRtn());
    }

    
    public void GameOver()
    {
        StartCoroutine(GameOverRtn());
    }


    public void UIActivasion()
    {

        joySticks.gameObject.SetActive(true);
        joySticks.GetComponent<CanvasGroup>().DOFade(1, 1f);
        uiBar.gameObject.SetActive(true);
        uiBar.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }


    IEnumerator GameOverRtn()
    {
        gameCanStart = false;
        yield return new WaitForSeconds(2f);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealthController>().EnemyDeathAtGameOver();
        }
    }


    IEnumerator GameStartRtn()
    {
        elevator.GetComponent<ElevatorController>().ElevatorMove();
        yield return new WaitForSeconds(4f);
        UIActivasion();
        yield return new WaitForSeconds(3f);
        StartCoroutine(EnemyBoxSpawning());
    }


    IEnumerator EnemyBoxSpawning()
    {
        if (gameCanStart)
        {
            enemySpawnController.SpawnEnemyBox();
            yield return new WaitForSeconds(enemyBoxSpawnCd);
            StartCoroutine(EnemyBoxSpawning());
        }
        
    }


}
