using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
   
    public GameObject obj;
    private int light_value = 200;
    private int enemy_value = -300;
   
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Item_light")
        {
           
            obj.GetComponent<ChangingNumber>().AddToNumber(light_value);
        }
        if (col.gameObject.tag == "enemy")
        {
            obj.GetComponent<ChangingNumber>().AddToNumber(enemy_value);
        }
    }
    
   
}
