using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkController : MonoBehaviour
{
    private Transform target;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("torch").transform;
        print(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
