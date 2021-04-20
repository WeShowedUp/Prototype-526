using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class chest : MonoBehaviour
{
    //where to get data for number of keys and level
    [SerializeField]
    private GameStatus status;

    //where to update the dash mechanic
    [SerializeField]
    private PlayerController controls;

    //popup messages for chest
    [SerializeField]
    private GameObject locked;
    [SerializeField]
    private GameObject opened;

    //where to spawn object from
    [SerializeField]
    private KeySpawner keyspawn;
    [SerializeField]
    private EnemySpawner enemyspawn;
    public Shop shop;
    public Text timer;

    
    private int coinsAwarded;

    public AudioClip ChestOpenAudio;

    void Start()
    {
        coinsAwarded=3;
        //status.coinCount += 3;
        status.levelStartTimer=Time.time; //reset level timer
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        //when the player goes near the chest
        if (collision.tag == "Player")
        {
            //check for number of keys
            //open
            if (status.keyCount>=1)
            {
                //say good job
                opened.SetActive(true);
                AudioSource.PlayClipAtPoint(ChestOpenAudio, transform.position, 3.0f);


                //award dash
                controls.powerGain++;

                //give any currencny rewards here
                GameStatus.coinCount += coinsAwarded;
                for (int i=0; i<coinsAwarded; i++){
                    
                    Analytics.CustomEvent("Get Coins", 
                        new Dictionary<string, object> { 
                            {"Level", status.getLevel()},
                            {"Type", "Level Win"}
                         }
                    );
                }


                //level win
                status.levelWin();
                Analytics.CustomEvent("LevelWin", new Dictionary<string, object> { {"Level", status.getLevel()}});

                //time on level
                Analytics.CustomEvent("Level End", 
                    new Dictionary<string, object> { 
                        {"Level", status.getLevel()},
                        {"Type", "Win"},
                        {"Time", Time.time-status.levelStartTimer}
                    }
                );

                //open shop
                shop.OpenShop();

                // destroy old keys
                Destroy(GameObject.FindGameObjectWithTag("key"));

                //destroy old stars
                GameObject[] stars = GameObject.FindGameObjectsWithTag("star");
                foreach (GameObject star in stars)
                {
                    Destroy(star);
                }

                //destroy old torch
                GameObject[] torchs = GameObject.FindGameObjectsWithTag("Torch");
                foreach (GameObject torch in torchs)
                {
                    Destroy(torch);
                }

                //pause the timer
                timer.GetComponent<Timer>().setEnd();

            }
            
            //dont open, say to get more keys
            else
            {
                //should put up a message saying to get more keys
                locked.SetActive(true);
            }
        }

    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        //when the olayer walks away from the chest the messages disapear
        if (collision.tag =="Player")
        {
            opened.SetActive(false);
            locked.SetActive(false);

            if (status.keyCount>=1){

                //reset keys to 0
                status.keyCount=0;
                
                // destroy old chest
                Destroy(this.gameObject);

                // destroy old keys
                Destroy(GameObject.FindGameObjectWithTag("key"));

                //spawn new keys and chest
                keyspawn.SpawnObjectAtRandom();

                //spawn new enemies
                enemyspawn.SpawnEnemyAtRandom();

                //remove good job
                opened.SetActive(false);

            }

            //resume timer
            timer.GetComponent<Timer>().setResume();

        }
    }
    // Start is called before the first frame update
   
}

