using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    private GameStatus key;

    void Start()
    {
        key = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStatus>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            key.keyCount++;
            Destroy(this.gameObject);
          
        }
    }
}
