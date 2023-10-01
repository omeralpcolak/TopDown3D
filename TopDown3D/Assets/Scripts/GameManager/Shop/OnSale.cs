using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnSale : MonoBehaviour
{
    public string itemName;
    public int itemPrice;

    public TMP_Text itemNameTxt;
    public TMP_Text itemPriceTxt;

    public void Initialize(string itemName, int itemPrice)
    {
        this.itemName = itemName;
        this.itemPrice = itemPrice;

        UpdateUI();
    }

    public void UpdateUI()
    {
        itemNameTxt.text = itemName;
        itemPriceTxt.text = itemPrice.ToString();


    }


    /*public void ItemInfo()
    {

        switch (itemName)
        {
            case "Viking Hat":

                itemNameTxt.text = itemName;
                itemPriceTxt.text = itemPrice.ToString();
                break;

            case "Wizard Hat":
                itemNameTxt.text = itemName;
                itemPriceTxt.text = itemPrice.ToString();
                break;

            case "Sleeping Hat":
                itemNameTxt.text = itemName;
                itemPriceTxt.text = itemPrice.ToString();
                break;

                   
        }
    }*/
}
