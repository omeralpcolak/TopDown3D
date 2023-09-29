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


    private void Start()
    {
        ItemInfo();
    }

    public void ItemInfo()
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
    }


}
