using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameStatus key;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            key.keyCount++;
            Destroy(this.gameObject);
          
        }
    }
}
