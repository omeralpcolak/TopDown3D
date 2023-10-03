using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public int killCount;
    public int wallet;

    public OnSale vikingHat;
    public OnSale wizardHat;
    public OnSale sleepingHat;

    void Start()
    {
        wallet = PlayerPrefs.GetInt("Wallet", 0);

        vikingHat.Initialize("Viking Hat", 150);
        wizardHat.Initialize("Wizard Hat", 300);
        sleepingHat.Initialize("Sleeping Hat", 10);
    }

    public void GainSoul()
    {

        killCount++;
        wallet += killCount;
        killCount = 0;
        SaveWallet();
    }

    public void Buy(OnSale item)
    {
        wallet -= item.itemPrice;
        SaveWallet();
        ScreenManager.instance.UpdateTexts();

    }

    public void SaveWallet()
    {
        PlayerPrefs.SetInt("Wallet", wallet);
        PlayerPrefs.Save();
    }
}
