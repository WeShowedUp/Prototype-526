using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvent : MonoBehaviour
{
   
    public GameObject obj;
    private int light_value = 200;
    private int enemy_value = -300;
    public Button pause_button;
    const float PAUSE_COOLDOWN_MAX = 10f;
    private float pause_cooldown = 0;
    
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

    void Update()
    {
        pause_cooldown -= Time.deltaTime;
        if(pause_cooldown < 0 && pause_button.interactable == false)
        {
            pause_button.interactable = true;
        }
    }

    public void PowerPauseTimer()
    {

        pause_cooldown = PAUSE_COOLDOWN_MAX;
        pause_button.interactable = false;
        StartCoroutine(PauseTimer(obj));

        
        
    }

    IEnumerator PauseTimer(GameObject obj)
    {
        float value = obj.GetComponent<ChangingNumber>().GetLightDecay();
        obj.GetComponent<ChangingNumber>().SetLightDecay(0);
        //pause the timer for 5 seconds
        yield return new WaitForSeconds(5f);

        obj.GetComponent<ChangingNumber>().SetLightDecay(value);
    }

    
    
   
}
