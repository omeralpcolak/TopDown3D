using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;

    private GameObject currentScreen;

    public GameObject mainMenuScene;
    public GameObject gameOverScene;
    public GameObject shopScene;

    public TMP_Text walletTxt;

    public List<Button> buttons = new List<Button>();


    private void Awake()
    {
        instance = this;
        currentScreen = mainMenuScene;
    }

    private void Start()
    {
        UpdateTexts();
        CheckWallet();
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
                UpdateTexts();
                CheckWallet();
                break;
        }

        currentScreen.SetActive(true);
        currentScreen.GetComponent<CanvasGroup>().DOFade(1, 1f);

    }

    void UpdateTexts()
    {
        walletTxt.text = GameManager.instance.wallet.ToString();
    }

    void CheckWallet()
    {
        int wallet = GameManager.instance.killCount;
        int cost = GameManager.instance.cost;

        foreach (Button button in buttons)
        {
            if (wallet < cost)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }

    }

    
}
