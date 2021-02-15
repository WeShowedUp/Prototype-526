using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    private Transform playerTransform;
    public float speed = 3f;
    private float speedInput;
    public float radius = 3f;
    public float changeDirectionTime = 5f;
    private float changeTimer;
    private bool HasLeavedOrigin = false;
    public bool isVertical;// 0: move up or down; 1: move left or right

    private Vector2 moveDirection;
    private Vector3 origin;
    private Rigidbody2D rbody;
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
        changeTimer = changeDirectionTime;
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
            } else if(HasLeavedOrigin && distance >= radius)
            {
                GoBackToOrigin();
                if (transform.position == origin)
                {
                    HasLeavedOrigin = false;
                    moveDirection = isVertical ? Vector2.up : Vector2.right;
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
            StartCoroutine(freeze(2.0f));
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
        Vector2 position = rbody.position;
        rbody.MovePosition(position + moveDirection * speed * Time.deltaTime);
    }
}
