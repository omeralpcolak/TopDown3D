using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelXpController : MonoBehaviour
{
    [SerializeField] float currentXp, maxXp;
    [SerializeField] float xpAmount;
    [SerializeField] float currentLevel, nextLevel;
    private float targetXp; //it is for xp bar.


    [SerializeField] TMP_Text currentLevelTxt;


    public Image xpImg;

    public GameObject xp;

    public bool canXpInstan;
    public float xpAddingSpeed; //in the xp bar.

    
    
    void Start()
    {
        canXpInstan = true;
        currentXp = 0;
        currentLevel = 1;
        nextLevel = currentLevel + 1;
    }

    
    void Update()
    {
        LevelUp();
        UpdateLevelUI();

        xpImg.fillAmount = Mathf.MoveTowards(xpImg.fillAmount, targetXp, xpAddingSpeed * Time.deltaTime);
    }


    public void GainXp()
    {
        currentXp += xpAmount;
    }

    public void LevelUp()
    {
        if(currentXp >= maxXp)
        {
            currentXp = 0;
            maxXp *=1.5f;
            currentLevel++;
            nextLevel++;
            UpdateXpBar();
        }
    }

    public void SpawnXp(Transform xpPos)
    {
        if (canXpInstan)
        {
            Instantiate(xp, xpPos.position, Quaternion.identity);
        }
    }

    public void UpdateXpBar()
    {
        targetXp = currentXp / maxXp;
    }

    private void UpdateLevelUI()
    {
        currentLevelTxt.text = "Level " + currentLevel.ToString();
    }

    
}
