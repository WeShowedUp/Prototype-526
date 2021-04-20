using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEvent : MonoBehaviour
{
   
    public GameObject obj;
    private Transform innerMask;
    private int light_value = 200;
    private int enemy_value = -300;
    public Button pause_button;
    public AudioClip footstep;
    //music
    public AudioClip getHit;

    private void Start()
    {
        innerMask = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().GetChild(1).GetChild(1);
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Item_light")
        {
           
            obj.GetComponent<ChangingNumber>().AddToNumber(light_value);
        }
        if (col.gameObject.tag == "enemy")
        {
            AudioSource.PlayClipAtPoint(getHit, transform.position, 3.0f);
            obj.GetComponent<ChangingNumber>().AddToNumber(enemy_value);
        }
        if (col.gameObject.tag == "Torch")
        {
            innerMask.localScale = new Vector3(50, 50, 1);
            StartCoroutine(LightAll(5.0f));
        }
    }



    public void PowerPauseTimer()
    {

        //pause_cooldown = PAUSE_COOLDOWN_MAX;
        //pause_button.interactable = false;
       // Debug.Log("here");
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

    IEnumerator LightAll(float time)
    {
        yield return new WaitForSeconds(time);
        innerMask.localScale = new Vector3(3, 3, 1);
    }

    public void Run()
    {
        AudioSource.PlayClipAtPoint(footstep, transform.position, 1000.0f);
    }
}
