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
    public GameObject pauseBtn;
    public GameObject pausedTxt;
    public GameObject player;
    public GameObject playerSpawnEffect;

    public float enemyBoxSpawnCd;

    public int totalKillCount;
    public int killCount;
    [HideInInspector]public int cost;
    [HideInInspector] public int wallet;

    [HideInInspector]public bool gameCanStart;

    PowerupController powerupController;
    EnemySpawnController enemySpawnController;

    Coroutine enemyBoxSpawningCoroutine;



    private void Awake()
    {
        instance = this;
        enemySpawnController = GetComponent<EnemySpawnController>();
        powerupController = GetComponent<PowerupController>();
    }

    private void Start()
    {
        ScreenManager.instance.ChangeScreen(Screen.MAIN);
        killCount = 0;
        StartCoroutine(EnemyBoxSpawning()); 
    }

    private void Update()
    {
        ControllingEnemyBoxSpawning();
        totalKillCount += killCount;
    }

    private void ControllingEnemyBoxSpawning()
    {
        if (gameCanStart)
        {
            if (powerupController.hasTeleported)
            {
                StopEnemyBoxSpawning();
            }
            else if (!powerupController.hasTeleported)
            {
                StartEnemyBoxSpawning();
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRtn());
    }

    public void GameStart()
    {
        StartCoroutine(GameStartRtn());
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

    public void Buy()
    {
        wallet -= cost;
        PlayerPrefs.SetInt("Wallet",totalKillCount);
    }

    public void Shop()
    {
        ScreenManager.instance.ChangeScreen(Screen.SHOP);
    }

    public void Return()
    {
        ScreenManager.instance.ChangeScreen(Screen.MAIN);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
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
        ScreenManager.instance.ChangeScreen(Screen.GAMEOVER);
    }

    

    IEnumerator GameStartRtn()
    {
        ScreenManager.instance.mainMenuScene.GetComponent<CanvasGroup>().DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        ScreenManager.instance.mainMenuScene.gameObject.SetActive(false);
        playerSpawnEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        player.transform.DOScale(0.75f, 1f);
        yield return new WaitForSeconds(1f);
        playerSpawnEffect.gameObject.SetActive(false);
        UIActivasion();
        yield return new WaitForSeconds(1.5f);
        gameCanStart = true;
    }

    private void StopEnemyBoxSpawning()
    {
        if (enemyBoxSpawningCoroutine != null)
        {
            StopCoroutine(enemyBoxSpawningCoroutine);
            enemyBoxSpawningCoroutine = null;
        }
    }

    private void StartEnemyBoxSpawning()
    {
        if(enemyBoxSpawningCoroutine == null)
        {
            enemyBoxSpawningCoroutine = StartCoroutine(EnemyBoxSpawning());
        }
    }

    private IEnumerator EnemyBoxSpawning()
    {
        while (gameCanStart)
        {
            enemySpawnController.SpawnEnemyBox();
            yield return new WaitForSeconds(enemyBoxSpawnCd);
        }
    }
}