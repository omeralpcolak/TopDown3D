using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelXpController : MonoBehaviour
{
    [SerializeField] float currentXp, maxXp;
    [SerializeField] float xpAmount;
    [SerializeField] int currentLevel, nextLevel;


    public Image xpImg;

    public float xpAddingSpeed; //in the xp bar.

    private float targetXp; //it is for xp bar.
    
    void Start()
    {
        currentXp = 0;
        currentLevel = 1;
        nextLevel = currentLevel + 1;
    }

    
    void Update()
    {
        LevelUp();

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
            maxXp *=2f;
            currentLevel++;
            nextLevel++;
            UpdateXpBar();
        }
    }

    public void UpdateXpBar()
    {
        targetXp = currentXp / maxXp;
    }
}
