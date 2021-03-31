using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class StarEvent : MonoBehaviour
{

    public GameStatus status;
    private int coinsAwarded;
    //private GameStatus gamestatus;

    //private int i;
    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameStatus>();
        coinsAwarded=2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
         GameStatus.coinCount += coinsAwarded; 

            //add a coin awarded event for each coin
            for( int i=0; i<coinsAwarded; i++ ){
                //status = GetComponent<GameStatus>();
                Analytics.CustomEvent("Get Coins", 
                    new Dictionary<string, object> { 
                        {"Level", status.getLevel()},
                        {"Type", "Star"}
                }
                );
            }


            Destroy(this.gameObject);
        }
    }
}
