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
    
    
   
    public void BUYACTION()
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
