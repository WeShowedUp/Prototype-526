using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSound : MonoBehaviour
{
    public AudioSource music;
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public void ButtonCliking()
    {
        //AudioSource.PlayClipAtPoint(buttonClick, transform.position, 100.0f);
        music.PlayOneShot(buttonClick);
    }
    public void ButtonHovering()
    {
        //AudioSource.PlayClipAtPoint(buttonClick, transform.position, 100.0f);
        music.PlayOneShot(buttonHover);
    }
}
