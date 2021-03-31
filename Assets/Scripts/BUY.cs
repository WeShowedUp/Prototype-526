﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class BUY : MonoBehaviour
{
    public GameStatus coin;
    public static int freezebombCount;
    public Text freezebombText;
    public Text CoinText;
    public GameObject warning;
    
    private int itemCost=0;

    private GameStatus gamestatus;
   
    public void BUYACTION()
    {
        if (int.Parse(freezebombText.text) == 5)
        {
            warning.SetActive(true);
        }
        else
        {
            if (GameStatus.coinCount >= itemCost)
            {
                GameStatus.coinCount -= itemCost;


                gamestatus = GetComponent<GameStatus>();

                //report coin spending
                for (int i =0; i<itemCost; i++){
                    Analytics.CustomEvent("Coins Spent", 
                        new Dictionary<string, object> { 
                            {"Level", gamestatus.getLevel()},
                            {"Type", "Item"}
                        }
                    );
                }

                //report item purchase
                gamestatus = GetComponent<GameStatus>();
                Analytics.CustomEvent("Purchase", 
                    new Dictionary<string, object> { 
                        {"Level", gamestatus.getLevel()},
                        {"Item", "Freeze Bomb"}
                    }
                );

                freezebombCount++;
            }

            freezebombText.text = freezebombCount.ToString();
            CoinText.text = GameStatus.coinCount.ToString();
        }
        

        
    }

}
