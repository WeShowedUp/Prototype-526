using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    
    public Text levelText;

    public GameStatus gamestatus;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        levelText.text = "Lv "+gamestatus.gameLevel.ToString();
    }
}
