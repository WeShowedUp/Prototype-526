using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIGHTR : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightPrefab;
    public int LightCount = 0;
    public int LightMaxNumber = 1;
    private float Timer;

    public float TimeReset = 5;

    void Start()
    {
        LightCount++;
        GameObject instance = GameObject.Instantiate(lightPrefab, transform.position, Quaternion.identity) as GameObject;
        instance.GetComponent<Light2>().RP = this;
        Timer = TimeReset;
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
                GameObject instance = GameObject.Instantiate(lightPrefab, pos, Quaternion.identity) as GameObject;

                instance.GetComponent<Light2>().RP = this;

                Timer = TimeReset;

            }
        }
    }
    public void minus()
    {
        this.LightCount--;
    }
}
