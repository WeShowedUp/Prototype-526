using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyAtRandom();
    }

    public void SpawnEnemyAtRandom()
    {
        Vector3 randompos;

        //spawn enemies, control number of spawned enemies by changing j
        for (int j = 0; j < 10; j++)
        {
            randompos = new Vector3(Random.Range(-44f, 52f), Random.Range(-24f, 22f), 0f);
            Instantiate(enemy, randompos, Quaternion.identity);
        }

    }
}
