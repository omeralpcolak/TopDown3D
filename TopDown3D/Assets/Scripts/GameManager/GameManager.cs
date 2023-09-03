using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject joySticks;
    public GameObject uiBar;
    public GameObject elevator;

    public float enemyBoxSpawnCd;

    public bool gameCanStart;

    [SerializeField] TMP_Text gameOverTxt;

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


    private void UIActivasion()
    {

        joySticks.gameObject.SetActive(true);
        joySticks.GetComponent<CanvasGroup>().DOFade(1, 1f);
        uiBar.gameObject.SetActive(true);
        uiBar.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    private void UIDeactivasion()
    {
        joySticks.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            joySticks.gameObject.SetActive(false);
        });

        uiBar.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            uiBar.gameObject.SetActive(false);
        });

    }


    IEnumerator GameOverRtn()
    {
        gameCanStart = false;
        yield return new WaitForSeconds(1f);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealthController>().EnemyDeathAtGameOver();
        }
        yield return new WaitForSeconds(1f);
        UIDeactivasion();
        yield return new WaitForSeconds(1f);
        gameOverTxt.GetComponent<CanvasGroup>().DOFade(1, 1f);

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
