using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float speed = 5.0f; // movement speed of the enemy 
    public int maxrange = 3; // enemy detection range
    private Rigidbody2D RB;
    private Vector2 movement;
    private Vector3 direction;
    private Vector3 distance;
    private Vector3 Origin;
    bool status = false; // false: patroling status, true: chasing status
    bool istop = false; // false: patroling is reaching top; true: reaching the top and ready to start going down.
    float sum; // the distance between player and enemy
    Vector3 top;
    Vector3 down;
    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        Origin = transform.position;
        top = transform.position;
        top.y += 5;
        down = transform.position;
        down.y -= 5;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
       
    }
    private void FixedUpdate()
    {
        distance = player.position - transform.position;
        sum = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y);

        if (sum < maxrange)
        {
            status = true;
            chase(movement);
        }
      
        else if (status && sum >= maxrange)
        {
            BackToOrigin(Origin);
            if (transform.position == Origin)
            {
                status = false;
            }
            
        }
        else
        {
            patrolling();
        }
       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BULLET")
        {
            Destroy(gameObject);
           
        }

    }
    private void chase(Vector2 direction)
    {
        RB.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
        
        
    }
    private void BackToOrigin(Vector3 Origin)
    {
       // RB.MovePosition(Origin);
        transform.position = Vector2.MoveTowards(transform.position, Origin, speed * Time.deltaTime);
    }
    private void patrolling()
    {
        if (!istop)
        {
            transform.position = Vector2.MoveTowards(transform.position, top, speed * Time.deltaTime);
            if (transform.position == top)
            {
                istop = true;
            }
        }
        else if (istop)
        {
            transform.position = Vector2.MoveTowards(transform.position, down, speed * Time.deltaTime);
            if (transform.position == down)
            {
                istop = false;
            }

        }


    }
}
