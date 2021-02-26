using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float DASH_COOLDOWN_MAX = 2f;
    private Rigidbody2D myRB;

    // dash mechanics
    public int powerGain; //the ability level the player has
    public float dashAmount = 3;
    
    public float dashCooldown;
    public Vector3 lastMove;

    private bool isDash;


    [SerializeField]
    public float speed = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();

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

            dashCooldown -= Time.deltaTime;

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
                    isDash = false;
                }
                isDash = false;
                
            }
            
        }
        
    }


}
