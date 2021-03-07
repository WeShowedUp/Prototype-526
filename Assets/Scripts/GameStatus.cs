using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public ChangingNumber light;
    public Text keyNumber;
    public Text coinNumber;
    public int keyCount = 0;
    public int coinCount = 0;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        keyNumber.text = keyCount.ToString();
        coinNumber.text = coinCount.ToString();
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
