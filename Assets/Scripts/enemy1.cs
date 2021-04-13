using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using UnityEngine.UI;

public class enemy1 : Enemy
{
    // Start is called before the first frame update
   
    public float speedInput = 2.5f; // movement speed of the enemy 
    public int maxrange = 3; // enemy detection range
    private Transform playerTransform;
    private Transform player;
    private Rigidbody2D RB;
    private Vector2 movement;
    private Vector3 direction;
    private Vector3 distance;
    private Vector3 Origin;
    private Animator anim;
    bool status = false; // false: patroling status, true: chasing status
    bool istop = false; // false: patroling is reaching top; true: reaching the top and ready to start going down.
    float sum; // the distance between player and enemy
    public float speed;
    Vector3 top;
    Vector3 down;
    public static bool E1pause = false;
    private GameStatus gamestatus;

    //music
    public AudioClip attackAudio;

    void Start()
    {
        health = 1;
        damage = 1;
        RB = this.GetComponent<Rigidbody2D>();
        Origin = transform.position;
        top = transform.position;
        top.y += 5;
        down = transform.position;
        down.y -= 5;
        speed = speedInput;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // base.Update();

        direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;

    }
    private void FixedUpdate()
    {
        distance = player.position - transform.position;
        sum = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y);
        if (!E1pause)
        { 
            if (sum < maxrange)
            {
                status = true;
                chase(movement);
                AnimatorSetWhenTowards(player.position);
            }

            else if (status && sum >= maxrange)
            {
                BackToOrigin(Origin);
                AnimatorSetWhenTowards(Origin);
                if (transform.position.x == Origin.x && transform.position.y == Origin.y)
                {
                    status = false;
                }

            }
            else
            {
                patrolling();
            }
        }
       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "BULLET")
        //{
        //    Destroy(gameObject);

        //}
        if (!E1pause)
        {
            if (collision.tag == "Player")
            {

                speed = 0;

                Analytics.CustomEvent("Patrolling Enemy");

                gamestatus = GetComponent<GameStatus>();
                AudioSource.PlayClipAtPoint(attackAudio, transform.position, 3.0f);

                StartCoroutine(freeze(2.0f));
                Analytics.CustomEvent("Enemy Hit",
                   new Dictionary<string, object> {
                    {"Level", gamestatus.getLevel()},
                    {"Type", "Patrolling"}
                   }
               );
            }
        }
    }
    IEnumerator freeze(float time)
    {
        yield return new WaitForSeconds(time);
        speed = speedInput;
    }
    private void chase(Vector2 direction)
    {
        //RB.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

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
            AnimatorSetWhenTowards(top);
            if (transform.position.x == top.x && transform.position.y == top.y)
            {
                istop = true;
            }
        }
        else if (istop)
        {
            transform.position = Vector2.MoveTowards(transform.position, down, speed * Time.deltaTime);
            AnimatorSetWhenTowards(down);
            if (transform.position.x == down.x && transform.position.y == down.y)
            {
                istop = false;
            }

        }


    }
    IEnumerator enemyfreeze(float time)
    {
      
        yield return new WaitForSeconds(time);
        E1pause =false;
      
    }
    private void AnimatorSetWhenTowards(Vector2 towards)
    {
        float xDiff = transform.position.x - towards.x;
        float yDiff = transform.position.y - towards.y;
        if (Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
        {
            if (xDiff < 0) anim.SetFloat("moveX", 1);
            else anim.SetFloat("moveX", -1);
            anim.SetFloat("moveY", 0);
        }
        else
        {
            if (yDiff < 0) anim.SetFloat("moveY", 1);
            else anim.SetFloat("moveY", -1);
            anim.SetFloat("moveX", 0);
        }
    }
}
