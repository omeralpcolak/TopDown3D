using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public List<Transform> spawnPos = new List<Transform>();
    public GameObject spawnBox;

    
    

    private int previousSpawnIndex = -1;



    public void SpawnEnemyBox()
    {
       
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, spawnPos.Count);
        } while (randomIndex == previousSpawnIndex);

        Transform randomSpawnPos = spawnPos[randomIndex];
        Instantiate(spawnBox, randomSpawnPos.position, Quaternion.identity);

        
        previousSpawnIndex = randomIndex;
    }
}
