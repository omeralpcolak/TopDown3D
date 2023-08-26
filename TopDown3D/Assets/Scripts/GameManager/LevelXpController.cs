using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelXpController : MonoBehaviour
{
    [SerializeField] float currentXp, maxXp;
    [SerializeField] int currentLevel, nextLevel;
    [SerializeField] float xpAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentXp = 0;
        currentLevel = 1;
        nextLevel = currentLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        LevelUp();
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
        }
    }
}
