using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;

    private GameObject currentScreen;

    public GameObject mainMenuScene;
    public GameObject gameOverScene;
    public GameObject shopScene;

    public TMP_Text walletTxt;

    private void Awake()
    {
        instance = this;
        currentScreen = mainMenuScene;
    }


    public void ChangeScreen(Screen screen)
    {
        currentScreen.GetComponent<CanvasGroup>().DOFade(0, 1f);
        currentScreen.SetActive(false);
        

        switch (screen)
        {
            case Screen.MAIN:
                currentScreen = mainMenuScene;
                break;

            case Screen.GAMEOVER:
                currentScreen = gameOverScene;
                break;

            case Screen.SHOP:
                currentScreen = shopScene;
                CheckTexts();
                CheckWallet();
                break;
        }

        currentScreen.SetActive(true);
        currentScreen.GetComponent<CanvasGroup>().DOFade(1, 1f);

    }

    void CheckTexts()
    {
        walletTxt.text = GameManager.instance.totalKillCount.ToString();
    }

    void CheckWallet()
    {
        int wallet = GameManager.instance.wallet;
        int cost = GameManager.instance.cost;

        if (wallet < cost)
        {
            //Button can not interactable.
        }
        else
        {
            //Button can be interactable.
        }

    }

    
}
