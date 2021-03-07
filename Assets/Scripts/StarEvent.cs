using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarEvent : MonoBehaviour
{

    public GameStatus coin;
    // Start is called before the first frame update
    void Start()
    {
        coin = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coin.coinCount += 2;
            Destroy(this.gameObject);
        }
    }
}
