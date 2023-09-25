using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;


    public GameObject mainMenuScene;
    public GameObject gameNameTxt;
    public GameObject playBtn;
    public GameObject quitBtn;

    public GameObject gameOverScene;
    public GameObject gameOverTxt;
    public GameObject restartBtn;

    public GameObject shopScene;
    public GameObject returnBtn;
    public GameObject hatDisplay1, hatDisplay2, hatDisplay3;

    private void Awake()
    {
        instance = this;
    }

    public void MainMenuScene()
    {
        StartCoroutine(MainMenuRtn());

    }

    public void GameOverScene()
    {
        StartCoroutine(GameOverSceneRtn());
    }

    public void ShopScene()
    {
        //StartCoroutine(ShopSceneRtn());
    }

    IEnumerator GameOverSceneRtn()
    {
        gameOverTxt.GetComponent<CanvasGroup>().DOFade(1f, 1f);
        yield return new WaitForSeconds(0.5f);
        restartBtn.gameObject.SetActive(true);
        restartBtn.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    IEnumerator MainMenuRtn()
    {
        gameNameTxt.GetComponent<CanvasGroup>().DOFade(1f, 1f);
        yield return new WaitForSeconds(0.5f);
        playBtn.GetComponent<CanvasGroup>().DOFade(1f, 1f);
        yield return new WaitForSeconds(0.5f);
        quitBtn.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }

    
}
