using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    const float DASH_COOLDOWN_MAX = 2f;
    const float PAUSE_COOLDOWN_MAX = 10f;
    
    // how many seconds the timer UI still stays active after the time is up 
    // set to 0 if you want timer to disappear immediately
    const float COOLDOWN_DISAPPEAR_MAX = 0f;
    
    private Rigidbody2D myRB;

    private GameStatus gamestatus;

    // dash mechanics
    public int powerGain; //the ability level the player has
    public float dashAmount = 3;
    
    public float dashCooldown;
    public float pauseCooldown = 0;

    public Vector3 lastMove;

    private bool isDash;
    private bool isAbility;

    public Text inventory;

    // cooldown timer UI components
    public Text cooldownText;
    public Image background;
    public Image cooldownImage;
    public Image ring;


    [SerializeField]
    public float speed = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //powerGain=0;

        enableCooldownTimer(false);        

    }

    // enable or disable cooldown timer UI
    void enableCooldownTimer(bool cool)
    {
        cooldownImage.enabled=cool;
        background.enabled=cool;
        ring.enabled=cool;
        cooldownText.enabled=cool;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerGain >= 0)
        {
            //speed allows it to move at a speed faster than 1 
            //time accounts for computers of different speeds
            //Raw axis makes the player move fast at the start rather than having to ramp up speed
            myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        }
        if (powerGain >= 1)
        {
            float moveX = 0f; 
            float moveY = 0f;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX = +1f;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveY = +1f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                moveY = -1f;
            }
            lastMove = new Vector2(moveX, moveY).normalized;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDash = true;
            }

            if (Input.GetKeyDown(KeyCode.Q) && int.Parse(inventory.text) > 0 && pauseCooldown < 0)
            {
                isAbility = true;
                int left = int.Parse(inventory.text) - 1;
                inventory.text = left.ToString();
            }


            dashCooldown -= Time.deltaTime;
            pauseCooldown -= Time.deltaTime;


            // display cooldown countdown timer.
            if (dashCooldown > 0)
            {
                enableCooldownTimer(true);
                cooldownText.text = Mathf.Ceil(dashCooldown).ToString();
            }
            else if (dashCooldown >= -COOLDOWN_DISAPPEAR_MAX)
            {
                cooldownText.text = "0";
            }
            else
            {
                enableCooldownTimer(false);
            }
            ring.fillAmount = dashCooldown / DASH_COOLDOWN_MAX;

        }
        

    }

    void FixedUpdate()
    {
        if(powerGain >= 1)
        {
            myRB.velocity = lastMove * speed;
            if (isDash)
            {
                if (dashCooldown < 0)
                {
                    dashCooldown = DASH_COOLDOWN_MAX;
                    myRB.MovePosition(transform.position + lastMove * dashAmount);

                    //count dashes
                    gamestatus = GetComponent<GameStatus>();
                    Analytics.CustomEvent("Dash", 
                        new Dictionary<string, object> { 
                            {"Level", gamestatus.getLevel()},
                           
                        }
                    );

                    
                    isDash = false;
                }
                isDash = false;
                
            }
            if (isAbility)
            {
                if(pauseCooldown < 0)
                {
                    pauseCooldown = PAUSE_COOLDOWN_MAX;

                    //count item use
                    gamestatus = GetComponent<GameStatus>();
                    Analytics.CustomEvent("Item Used", 
                        new Dictionary<string, object> { 
                            {"Level", gamestatus.getLevel()},
                            {"Item", "FreezeBomb"}
                        }
                    );
                
                    GetComponent<PlayerEvent>().PowerPauseTimer();
                    isAbility = false;
                }
            }
        }
        
    }


}
