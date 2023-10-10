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
    public OnSale mexicanHat;

    public GameObject vikingHatObj;
    public GameObject wizardHatObj;
    public GameObject sleepingHatObj;
    public GameObject mexicanHatObj;

    public GameObject equippedObj; 

    private const string EquippedItemKey = "EquippedItem";

    void Start()
    {
        wallet = PlayerPrefs.GetInt("Wallet", 0);

        vikingHat.Initialize("Viking Hat", 10, vikingHatObj);
        wizardHat.Initialize("Wizard Hat", 10, wizardHatObj);
        sleepingHat.Initialize("Sleeping Hat", 10, sleepingHatObj);
        mexicanHat.Initialize("Sombrero", 0, mexicanHatObj);

        string equippedItemName = PlayerPrefs.GetString(EquippedItemKey, "");
        if (!string.IsNullOrEmpty(equippedItemName))
        {
            OnSale equippedItem = FindItemByName(equippedItemName);
            if (equippedItem != null)
            {
                EquipItem(equippedItem);
            }
        }
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
        UnequipItem();
        EquipItem(item);
        PlayerPrefs.SetString(EquippedItemKey, item.itemName);
        PlayerPrefs.Save();
    }

    public void SaveWallet()
    {
        PlayerPrefs.SetInt("Wallet", wallet);
        PlayerPrefs.Save();
    }

    private void EquipItem(OnSale item)
    {
        if (item.itemObj != null)
        {
            item.itemObj.SetActive(true);
            equippedObj = item.itemObj;
        }
    }

    private void UnequipItem()
    {
        if (equippedObj != null)
        {
            equippedObj.SetActive(false);
            equippedObj = null;
        }
    }

    private OnSale FindItemByName(string itemName)
    {
        if(itemName == mexicanHat.itemName)
        {
            return mexicanHat;
        }

        if (itemName == vikingHat.itemName)
        {
            return vikingHat;
        }
        else if (itemName == wizardHat.itemName)
        {
            return wizardHat;
        }
        else if (itemName == sleepingHat.itemName)
        {
            return sleepingHat;
        }
        return null;
    }
}
