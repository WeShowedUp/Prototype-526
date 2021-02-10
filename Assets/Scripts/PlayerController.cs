using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D myRB;

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
        //speed allows it to move at a speed faster than 1 
        //time accounts for computers of different speeds
        //Raw axis makes the player move fast at the start rather than having to ramp up speed
        myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))* speed; 
    }
}
