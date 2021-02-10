using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public GameObject BULLET;
    public float speed = 10.0f;
    
    void Start()
    {
        rb.velocity = new Vector2(-speed, 0);
        Destroy(BULLET, 1.0f);
       
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "")
        {
            

        }
       
    }

}
