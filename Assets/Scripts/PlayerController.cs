using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour
{
    const float DASH_COOLDOWN_MAX = 5f;
    const float PAUSE_COOLDOWN_MAX = 10f;
    
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
    public BUY buy;

    // dashing cooldown timer UI components
    public Text dashCooldownText;
    public Image dashCooldownMask;

    // pause cooldown timer UI components
    public Text pauseCooldownText;
    public Image pauseCooldownMask;


    [SerializeField]
    public float speed = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //powerGain=0;

        enableCooldownTimer(false); 
        enablePauseCooldownTimer(false);       

    }

    // enable or disable dashing cooldown timer UI
    void enableCooldownTimer(bool cool)
    {
        dashCooldownMask.enabled=cool;
        dashCooldownText.enabled=cool;
    }

    // enable or disable bombing (pausing) cooldown timer UI
    void enablePauseCooldownTimer(bool cool)
    {
        pauseCooldownText.enabled=cool;
        pauseCooldownMask.enabled=cool;
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
                Analytics.CustomEvent("Dash");
                isDash = true;
            }
            if (Input.GetKeyDown(KeyCode.Q) && int.Parse(inventory.text) > 0 && pauseCooldown < 0)
            {
                isAbility = true;

                Analytics.CustomEvent("Freeze Bomb");
                
                
                int left = int.Parse(inventory.text) - 1;
                inventory.text = left.ToString();
                buy.freezebombCount--;
            }


            dashCooldown -= Time.deltaTime;
            pauseCooldown -= Time.deltaTime;


            // display dash cooldown countdown timer.
            if (dashCooldown > 0)
            {
                enableCooldownTimer(true);
                dashCooldownText.text = Mathf.Ceil(dashCooldown).ToString();
            }
            else
            {
                enableCooldownTimer(false);
            }
            dashCooldownMask.fillAmount = dashCooldown / DASH_COOLDOWN_MAX;


            // display bomb (pause) cooldown countdown timer.
            if (pauseCooldown > 0)
            {
                enablePauseCooldownTimer(true);
                pauseCooldownText.text = Mathf.Ceil(pauseCooldown).ToString();
            }
            else
            {
                enablePauseCooldownTimer(false);
            }
            pauseCooldownMask.fillAmount = pauseCooldown / PAUSE_COOLDOWN_MAX;

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
                    GetComponent<PlayerEvent>().PowerPauseTimer();
                    isAbility = false;

                    Analytics.CustomEvent("Item Used", 
                        new Dictionary<string, object> { 
                            {"Level", gamestatus.getLevel()},
                            {"Item", "FreezeBomb"}
                        }
                    );
                }
            }
        }
        
    }


}
