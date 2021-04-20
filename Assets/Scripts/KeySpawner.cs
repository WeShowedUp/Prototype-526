using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject key;
    public GameObject box;
    public GameObject star;
    public GameObject torch;

    void Start()
    {
        SpawnObjectAtRandom();
    }

    public void SpawnObjectAtRandom()
    {
        //set position limit to the size of the map
        Vector3 randompos;

        //spawn two keys
        for (int i = 0; i < 2; i++)
        {
            randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
            Instantiate(key, randompos, Quaternion.identity);
        }

        //spawn box
        GameObject chest;
        randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
        chest = Instantiate(box, randompos, Quaternion.identity);
        chest.tag = "Chest";
        chest.SetActive(true);

        //spawn a star
        randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
        Instantiate(star, randompos, Quaternion.identity);

        //spawn a torch
        randompos = new Vector3(Random.Range(-44f, 56f), Random.Range(-24f, 26f), 0f);
        Instantiate(torch, randompos, Quaternion.identity);
    }
}