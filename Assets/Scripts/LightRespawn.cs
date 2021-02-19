using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightPrefab;
    public int LightCount = 0;
    public int LightMaxNumber = 1;
    private float Timer = 20;
   
    private float TimeReset;

    void Start()
    {
        LightCount++;
        GameObject instance = GameObject.Instantiate(lightPrefab, transform.position, Quaternion.identity) as GameObject;
        instance.GetComponent<lightEvent>().Respawnpoint = this;
        TimeReset = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (LightCount < LightMaxNumber)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                Vector3 pos = transform.position;
                pos.x += Random.Range(-5, 5);
                pos.y += Random.Range(-5, 5);
                LightCount++;
                GameObject instance = GameObject.Instantiate(lightPrefab, pos, Quaternion.identity)as GameObject;

                instance.GetComponent<lightEvent>().Respawnpoint = this;

                Timer = TimeReset;

                
            }
        }
    }
    public void minus()
    {
        this.LightCount--;
    }
  
}
