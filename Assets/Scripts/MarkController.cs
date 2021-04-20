using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkController : MonoBehaviour
{
    public string targetTag = "Chest";
    private GameObject target;
    private Transform targetTransform;
    private Transform playerTransform;
    private bool hasTarget = false;

    public float x0 = 4.88f;
    public float y0 = 2.63f;
    public float radius = 10;
    private float k;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        k = y0 / x0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!hasTarget) {
            target = GameObject.FindGameObjectWithTag(targetTag);
            transform.localPosition = new Vector3(0, 0, -100);
        }
        if (target)
        {
            hasTarget = true;
            targetTransform = target.transform;
            float distance = (targetTransform.position - playerTransform.position).magnitude;
            if (distance > radius)
            {
                SetMarkAccordingTo(targetTransform.position, playerTransform.position);
            }
            else transform.localPosition = new Vector3(0, 0, -100);
        }
        else hasTarget = false;

    }

    private void SetMarkAccordingTo(Vector2 target, Vector2 player)
    {
        float f1 = k * (player.x - target.x) + target.y - player.y;
        float f2 = k * (target.x - player.x) + target.y - player.y;
        float outX = 0;
        float outY = 0;
        if (f1 > 0 && f2 > 0)
        {
            outX = (target.x - player.x) * y0 / (target.y - player.y);
            outY = y0;
        } else if(f1 < 0 && f2 < 0)
        {
            outX = (player.x - target.x) * y0 / (target.y - player.y);
            outY = -y0;
        } else if(f1 > 0 && f2 < 0)
        {
            outY = (target.y - player.y) * x0 / (player.x - target.x);
            outX = -x0;
        } else if(f1 < 0 && f2 > 0)
        {
            outY = (target.y - player.y) * x0 / (target.x - player.x);
            outX = x0;
        } else if(f1 == 0)
        {
            if (f2 > 0) (outX, outY) = (x0, y0);
            else (outX, outY) = (-x0, -y0);
        } else if(f2 == 0)
        {
            if (f1 > 0) (outX, outY) = (-x0, y0);
            else (outX, outY) = (x0, -y0);
        }
        transform.localPosition = new Vector2(outX, outY);
    }
}
