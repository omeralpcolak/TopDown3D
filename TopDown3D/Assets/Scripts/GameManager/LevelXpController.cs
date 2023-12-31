using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelXpController : MonoBehaviour
{
    [SerializeField] float currentXp, maxXp;
    [SerializeField] float xpAmount;
    public float currentLevel, nextLevel;

    private float targetXp; //it is for xp bar.

    [SerializeField] TMP_Text currentLevelTxt;

    public Image xpImg;

    public GameObject xp;
    public GameObject levelUpEffect;
    public GameObject levelUpTxtEffect;
    public GameObject portal;

    public bool canXpInstan;
    public bool portalSpawned = false;

    public float xpAddingSpeed; //in the xp bar.
    public float levelThresHold;

    public Transform portalPos;

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
        SpawnPortal();

        DestroyXps();
    }


    void SpawnPortal()
    {

        if (currentLevel % levelThresHold == 0 && !portalSpawned)
        {
            Instantiate(portal, portalPos.position, portalPos.rotation);
            portalSpawned = true;
        }

        if (currentLevel % levelThresHold != 0)
        {
            portalSpawned = false;
        }

    }

    void DestroyXps()
    {
        if (!GameManager.instance.gameCanStart)
        {
            GameObject[] xpObjs = GameObject.FindGameObjectsWithTag("Xp");

            foreach(GameObject xpObj in xpObjs)
            {
                Destroy(xpObj);
            }
        }
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
            StartCoroutine(LevelUpEffect());   
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

    IEnumerator LevelUpEffect()
    {
        levelUpEffect.gameObject.SetActive(true);
        levelUpTxtEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        levelUpEffect.gameObject.SetActive(false);
        levelUpTxtEffect.gameObject.SetActive(false);
    }
    
}
