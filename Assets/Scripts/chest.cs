using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest : MonoBehaviour
{
    //where to get data for number of keys
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
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //when the player goes near the chest
        if (collision.tag == "Player")
        {
            //check for number of keys
            //open
            if (status.keyCount==3)
            {
                //say good job
                opened.SetActive(true);

                

                //award dash
                controls.powerGain++;

                //give any currencny rewards here

                //spawn new keys and chest
                keyspawn.SpawnObjectAtRandom();

                //spawn new enemies
                enemyspawn.SpawnEnemyAtRandom();
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

            if (status.keyCount==3){

                //reset keys to 0
                status.keyCount=0;

                // destroy old chest
                Destroy(this.gameObject);

                //remove good job
                opened.SetActive(false);

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
