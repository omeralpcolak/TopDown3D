using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmallCubes : MonoBehaviour
{

    public float timeToDeath;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SmallCubesDeath());
    }

    IEnumerator SmallCubesDeath()
    {
        yield return new WaitForSeconds(timeToDeath);
        GameManager.instance.GetComponent<LevelXpController>().SpawnXp(transform);
        transform.DOScale(0f, 1f).OnComplete(delegate
        {
            Destroy(gameObject);
        });
    }
}
