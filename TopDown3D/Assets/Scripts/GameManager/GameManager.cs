using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject joySticks;
    public GameObject uiBar;
    public GameObject elevator;
    public GameObject restartBtn;
    public GameObject pauseBtn;
    public GameObject pausedTxt;

    public float enemyBoxSpawnCd;

    public bool gameCanStart;

    PowerupController powerupController;


    [SerializeField] TMP_Text gameOverTxt;

    EnemySpawnController enemySpawnController;


    private void Awake()
    {
        instance = this;
        enemySpawnController = GetComponent<EnemySpawnController>();
        powerupController = GetComponent<PowerupController>();
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
        pauseBtn.gameObject.SetActive(true);
        pauseBtn.GetComponent<CanvasGroup>().DOFade(1, 1f);
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

        pauseBtn.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            pauseBtn.gameObject.SetActive(false);
        });

    }

    public void ResumeAndPause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pausedTxt.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pausedTxt.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
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
        yield return new WaitForSeconds(1f);
        restartBtn.gameObject.SetActive(true);
        restartBtn.GetComponent<CanvasGroup>().DOFade(1, 1f);

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
