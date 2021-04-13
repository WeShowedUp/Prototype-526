using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private GameStatus key;
    public AudioClip getKey;
    void Start()
    {
        key = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(getKey, transform.position, 3.0f);
            key.keyCount++;
            Destroy(this.gameObject);

        }
    }
}