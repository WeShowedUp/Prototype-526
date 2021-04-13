using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public ChangingNumber light;
    public Text keyNumber;
    public int keyCount = 0;
    public Text coinNumber;
    public static int coinCount = 0;

    public int gameLevel;
    public float levelStartTimer;

    void Start()
    {
        gameLevel=0;
        Time.timeScale = 1;
        levelStartTimer=Time.timeSinceLevelLoad;
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
        //gameLevel=0;
        //levelStartTimer=Time.time;
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    void end()
    {
        //we might need to reset items and coins here?
        
        if (light.lightPoints <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void levelWin(){
        gameLevel++;
    }
    
    public int getLevel(){
        return gameLevel;
    }
}
