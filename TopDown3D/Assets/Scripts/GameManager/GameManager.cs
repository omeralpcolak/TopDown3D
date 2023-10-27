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

    public Vector3 shopSceneOffset;

    public float enemyBoxSpawnCd;

    public int killCountPerSession;

    [HideInInspector]public bool gameCanStart;
    [HideInInspector]public bool bossLevelStart;

    PowerupController powerupController;
    EnemySpawnController enemySpawnController;
    ShopManager shopManager;

    public BossLevelManager bossLevelManager;
    public CameraController cameraController;

    Coroutine enemyBoxSpawningCoroutine;


    private void Awake()
    {
        instance = this;
        enemySpawnController = GetComponent<EnemySpawnController>();
        powerupController = GetComponent<PowerupController>();
        shopManager = GetComponent<ShopManager>();
    }

    private void Start()
    {
        ScreenManager.instance.ChangeScreen(Screen.MAIN);
        killCountPerSession = 0;
    }


    private void Update()
    {
        ControllingEnemyBoxSpawning();
        ControllingBossLevel();
    }


    private void ControllingBossLevel()
    {
        if (killCountPerSession == 10 && !bossLevelStart)
        {
            bossLevelStart = true;
            KillEnemies();
            bossLevelManager.BeginningBossLevel();
        }

        if (killCountPerSession != 10)
        {
            bossLevelStart = false;
        }
    }


    private void ControllingEnemyBoxSpawning()
    {
        if (gameCanStart)
        {
            if (powerupController.hasTeleported || bossLevelStart)
            {
                KillEnemies();
                StopEnemyBoxSpawning();
            }
            else if (!powerupController.hasTeleported || !bossLevelStart)
            {
                StartEnemyBoxSpawning();
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRtn());
        shopManager.SaveWallet();
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

    public void Shop()
    {
        ScreenManager.instance.ChangeScreen(Screen.SHOP);
        cameraController.ChangeOffset(shopSceneOffset);
    }

    public void Victory()
    {
        StartCoroutine(VictoryRtn());
        
    }

    public void Return()
    {
        ScreenManager.instance.ChangeScreen(Screen.MAIN);
        cameraController.ResetOffset();
    }

    public void Continue()
    {
        gameCanStart = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        //ScreenManager.instance.ChangeScreen(Screen.MAIN);
    }

    public void Quit()
    {
        Application.Quit();
    }


    IEnumerator VictoryRtn()
    {
        KillEnemies();
        yield return new WaitForSeconds(1f);
        UIDeactivasion();
        yield return new WaitForSeconds(1f);
        ScreenManager.instance.ChangeScreen(Screen.VICTORY);
        gameCanStart = false;
    }
    

    IEnumerator GameOverRtn()
    {
        gameCanStart = false;
        yield return new WaitForSeconds(1f);
        KillEnemies();
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
        player.gameObject.SetActive(true);
        //yield return new WaitForSeconds(1f);
        //cameraController.ChangeOffset(playingOffset);
        yield return new WaitForSeconds(1f);
        UIActivasion();
        yield return new WaitForSeconds(1f);
        gameCanStart = true;
    }

    public void KillEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealthController>().EnemyDeathAtGameOver();
        }
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