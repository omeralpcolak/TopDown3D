using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OnSale : MonoBehaviour
{
    public string itemName;

    public int itemPrice;

    public GameObject itemObj;

    public TMP_Text itemNameTxt;
    public TMP_Text itemPriceTxt;

    public void Initialize(string itemName, int itemPrice, GameObject itemObj)
    {
        this.itemName = itemName;
        this.itemPrice = itemPrice;
        this.itemObj = itemObj;

        UpdatePrices();
    }

    public void UpdatePrices()
    {
        itemNameTxt.text = itemName;
        itemPriceTxt.text = itemPrice.ToString();


    }
}
