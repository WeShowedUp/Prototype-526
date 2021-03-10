using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BUY : MonoBehaviour
{
    public GameStatus coin;
    public int freezebombCount;
    public Text freezebombText;
    public Text CoinText;
    public GameObject warning;
    
    
   
    public void BUYACTION()
    {
        if (int.Parse(freezebombText.text) == 5)
        {
            warning.SetActive(true);
        }
        else
        {
            if (coin.coinCount >= 3)
            {
                coin.coinCount -= 3;
                freezebombCount++;
            }

            freezebombText.text = freezebombCount.ToString();
            CoinText.text = coin.coinCount.ToString();
        }
        

        
    }

}
