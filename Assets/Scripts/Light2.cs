using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light2 : MonoBehaviour
{
    public LIGHTR RP;
    public AudioClip getLight;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(getLight, transform.position, 3.0f);
            Destroy(this.gameObject);
            RP.minus();
        }
    }
}
