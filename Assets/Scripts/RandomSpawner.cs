using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject key;
    public GameObject box;

    void Start()
    {
        SpawnObjectAtRandom();
    }

    void SpawnObjectAtRandom()
    {
        //set position limit to the size of the map
        Vector3 randompos;

        //spawn three keys
        for (int i = 0; i < 3; i++)
        {
            randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
            Instantiate(key, randompos, Quaternion.identity);
        }

        //spawn box
        randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
        Instantiate(box, randompos, Quaternion.identity);

    }
}
