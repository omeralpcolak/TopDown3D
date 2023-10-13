using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossLevelManager : MonoBehaviour
{
    public List<GameObject> staircases = new List<GameObject>();
    public List<GameObject> staircaseObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StaircaseMovement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StaircaseMovement()
    {
        StartCoroutine(StaircaseMovementRtn());
        StartCoroutine(StaircaseObjectsMovementRtn());
        
    }

    IEnumerator StaircaseMovementRtn()
    {
        yield return new WaitForSeconds(6f);
        foreach (GameObject staircase in staircases)
        {
            staircase.transform.DOMoveY(0f, 1f);
            yield return new WaitForSeconds(.4f);
        }
        
    }

    IEnumerator StaircaseObjectsMovementRtn()
    {
        yield return new WaitForSeconds(6f);

        foreach (GameObject staircaseObject in staircaseObjects)
        {
            staircaseObject.transform.DOMoveY(0f, 1f);
            yield return new WaitForSeconds(.4f);
        }
    }

}
