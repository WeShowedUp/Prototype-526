using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyBat : Enemy
{
    private Transform playerTransform;
    public float speed = 2.5f;
    public float patrolSpeed = 0.5f;
    private float speedInput;
    public float radius = 3f;
    public float changeDirectionTime = 1f;
    private float changeTimer;
    private bool HasLeavedOrigin = false;
    public bool isVertical;// 0: move up or down; 1: move left or right
    public bool goLeftOrUp;// 0: move right or down; 1: move left or up
    private Vector2 moveDirection;
    private Vector2 InitDirection;
    private Vector3 origin;
    private Rigidbody2D rbody;
    private Animator anim;

    private GameStatus gamestatus;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        health = 1;
        damage = 1;
        speedInput = speed;
        origin = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveDirection = isVertical ? Vector2.up : Vector2.right;
        moveDirection = goLeftOrUp ? (-moveDirection) : moveDirection;
        InitDirection = moveDirection;
        changeTimer = changeDirectionTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if(distance < radius)
            {
                HasLeavedOrigin = true;
                Chase();
               // AnimatorSetWhenTowards(playerTransform.position);
            } else if(HasLeavedOrigin && distance >= radius)
            {
                GoBackToOrigin();
                //AnimatorSetWhenTowards(origin);
                if (transform.position == origin)
                {
                    HasLeavedOrigin = false;
                    moveDirection = InitDirection;
                    changeTimer = changeDirectionTime;
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
        if (collision.tag == "Player")
        {
            speed = 0;
            Analytics.CustomEvent("Guarding Enemy");
            gamestatus = GetComponent<GameStatus>();
            

            StartCoroutine(freeze(2.0f));
       


             Analytics.CustomEvent("Enemy Hit",
               new Dictionary<string, object> {
                 {"Level", gamestatus.getLevel()},
                {"Type", "Guarding"}
            }
            );
        }

    }
    IEnumerator freeze(float time)
    {
        yield return new WaitForSeconds(time);
        speed = speedInput;
       
    }
    private void GoBackToOrigin()
    {
        transform.position = Vector2.MoveTowards(transform.position, origin, speed * Time.deltaTime);
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }
    private void patrolling()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }
        //Vector2 position = rbody.position;
        //rbody.MovePosition(position + moveDirection * speed * Time.deltaTime);
        Vector2 position = transform.position;
        position += moveDirection * patrolSpeed * Time.deltaTime;
        transform.position = position;
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }
    private void AnimatorSetWhenTowards(Vector2 towards)
    {
        float xDiff = transform.position.x - towards.x;
        float yDiff = transform.position.y - towards.y;
        if(Mathf.Abs(xDiff) > Mathf.Abs(yDiff))
        {
            if (xDiff < 0) anim.SetFloat("moveX", 1);
            else anim.SetFloat("moveX", -1);
            anim.SetFloat("moveY", 0);
        } else
        {
            if (yDiff < 0) anim.SetFloat("moveY", 1);
            else anim.SetFloat("moveY", -1);
            anim.SetFloat("moveX", 0);
        }
    }
    public void paueseEnemy(float time)
    {
       
        speed = 0;
        patrolSpeed = 0;
        Debug.Log("pauseE");
        //StartCoroutine(freeze(time));
    }
}
