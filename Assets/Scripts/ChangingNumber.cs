using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class ChangingNumber : MonoBehaviour
{
    public GameObject message;

    public Text currency; //reference to the UI text object
   
    public float lightPoints; //the true numerical light value
    private const float STARTING_LIGHT_POINTS = 600; //HAVE TO CHANGE IN CANVAS TOO number of light points the player starts with
    
    //for the animation
    private float currNumber; //currNumber is the currently displayed light value
    private float animationTime = 1.5f;
    private float initial; //goal number is what display is changing to, intial is the start

    //for the timer
    private float timerSpeed = 1f;
    private float elapsed;
    private float lightDecay = 50;

    private float deathTime;


    //for anayltics
    private GameStatus gamestatus;

    public void AddToNumber(int value)
    {
        initial = currNumber;
        lightPoints += value;
    }

    public void SubToNumber(int value)
    {
        initial = currNumber;
        lightPoints -= value;
    }

    public void SetToZero()
    {
        initial = currNumber;
        lightPoints = 0;
    }

    //allows other parts of the code to get the current light point value
    public float getLightPts()
    {
        return lightPoints;
    }
    
    //functions for powerups
    public void SetLightDecay(float value)
    {
        lightDecay = value;
    }

    public float GetLightDecay()
    {
        return lightDecay;
    }
    // ticks the currency display up or down on screen
    private void adjustCurrencyDisplay()
    {
        
        if (currNumber != lightPoints)
        {
            float diff;
            if (initial < lightPoints)
            {
                diff = lightPoints - initial;
                currNumber += (animationTime * Time.deltaTime) * diff;
                if (currNumber >= lightPoints) //erin is assuming this is to account for rounding errors
                    currNumber = lightPoints;
            }
            else
            {
                diff = initial - lightPoints;
                currNumber -= (animationTime * Time.deltaTime) * diff;
                if (currNumber <= lightPoints) //erin is assuming this is to account for rounding errors
                    currNumber = lightPoints;
            }

            //dieng happens here
            if (currNumber <= 0)
            {
                gamestatus = GetComponent<GameStatus>();
                //deathTime= gamestatus.levelStartTimer - Time.time;
                
                
                currNumber = 0;
                currency.text = currNumber.ToString("0");

                showMessage();
                Time.timeScale = 0;
                gamestatus.levelStartTimer=Time.time;
                
                
                
                Analytics.CustomEvent("Died", 
                    new Dictionary<string, object> { 
                        {"Level", gamestatus.getLevel()}
                        
                    }
                );

                /*
                
                 //time on level
                Analytics.CustomEvent("Level End", 
                    new Dictionary<string, object> { 
                        {"Level", gamestatus.getLevel()},
                        {"Type", "Lose"},
                        {"Time", deathTime}
                    }
                );

                */
                
                
               

            }
            currency.text = currNumber.ToString("0");
        }
    }
    void showMessage()
    {
        message.SetActive(true);
    }
    public void Start()
    {
        lightPoints=STARTING_LIGHT_POINTS;
        currNumber=STARTING_LIGHT_POINTS;
        initial=STARTING_LIGHT_POINTS;
        gamestatus = GetComponent<GameStatus>();

    }


    public void Update()
    {
        //lose light over time
        elapsed += Time.deltaTime; //adds how much time ahs passed
        if (elapsed >= timerSpeed)
        {
            elapsed=0f;
            lightPoints-=lightDecay;
        }


        adjustCurrencyDisplay();
    }
}
