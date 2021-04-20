using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LightRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject lightPrefab;
    public GameObject Light;
    public int LightCount = 0;
    public int LightMaxNumber = 1;
    private float Timer;
   // public Text inventory;
    public static int BOMB;
    public float TimeReset = 5;

    void Start()
    {
        LightCount++;
        //light= GameObject.FindGameObjectWithTag("Item_light");
        GameObject instance = GameObject.Instantiate(lightPrefab, transform.position, Quaternion.identity) as GameObject;
        instance.GetComponent<lightEvent>().Respawnpoint = this;
        Timer = TimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        //BOMB = int.Parse(inventory.text);
       // Debug.Log("bomb" + BOMB);
        if (LightCount < LightMaxNumber)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                //Vector3 pos = transform.position;
                //pos.x += Random.Range(-5, 5);
                //pos.y += Random.Range(-5, 5);
                LightCount++;
                GameObject instance = GameObject.Instantiate(Light, transform.position, Quaternion.identity) as GameObject;
                instance.GetComponent<light>().RP = this;
                //GameObject instance = GameObject.Instantiate(lightPrefab.transform.GetChild(0), pos, Quaternion.identity)as GameObject;
                //instance.GetComponent<lightEvent>().Respawnpoint = this;

                Timer = TimeReset;

                
            }
        }
    }
    public void minus()
    {
        this.LightCount--;
    }
  
}
