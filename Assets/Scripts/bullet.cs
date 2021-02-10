using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   
    public Transform player;
    public GameObject bulletprefab;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
               
            }

    }
    void shoot()
    {
       Instantiate(bulletprefab, player.position, player.rotation);  
    }
}
