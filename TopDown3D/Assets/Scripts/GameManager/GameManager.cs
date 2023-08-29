using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject joySticks;
    public GameObject uiBar;
    public GameObject elevator;

    public float enemyBoxSpawnCd;

    EnemySpawnController enemySpawnController;


    private void Awake()
    {
        enemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
        StartCoroutine(GameStartRtn());
    }

    



    IEnumerator GameStartRtn()
    {
        elevator.GetComponent<ElevatorController>().ElevatorMove();
        yield return new WaitForSeconds(4f);
        UIActivasion();
        yield return new WaitForSeconds(3f);
        StartCoroutine(EnemyBoxSpawning());
    }
    

    public void UIActivasion()
    {

        joySticks.gameObject.SetActive(true);
        joySticks.GetComponent<CanvasGroup>().DOFade(1, 1f);
        uiBar.gameObject.SetActive(true);
        uiBar.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    IEnumerator EnemyBoxSpawning()
    {
        enemySpawnController.SpawnEnemyBox();
        yield return new WaitForSeconds(enemyBoxSpawnCd);
        StartCoroutine(EnemyBoxSpawning());
    }

}
