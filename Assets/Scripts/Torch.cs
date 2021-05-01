using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public AudioClip TorchAudio;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(TorchAudio, transform.position, 1000.0f);
            Destroy(this.gameObject);
        }
    }
    
}
