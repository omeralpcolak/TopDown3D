using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;

    private GameObject currentScreen;

    public GameObject mainMenuScene;
    public GameObject gameOverScene;
    public GameObject shopScene;

    private void Awake()
    {
        instance = this;
        currentScreen = mainMenuScene;
    }


    public void ChangeScreen(Screen screen)
    {
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
                break;
        }

        currentScreen.SetActive(true);
        currentScreen.GetComponent<CanvasGroup>().DOFade(1, 1f);


    }

    
}
