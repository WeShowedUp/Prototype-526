using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public ChangingNumber light;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //end();
    }
    public void Restart()
    {
        
        Application.LoadLevel(0);
        
    }
    void end()
    {
        if (light.lightPoints <= 0)
        {
            Time.timeScale = 0;
        }
    }
}
