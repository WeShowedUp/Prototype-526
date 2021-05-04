using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyMarksController : MonoBehaviour
{
    public string targetTag = "star";
    public GameObject targetPrefab;
    private GameObject[] targets;
    public bool hasTarget = false;
    private bool hasCreated = false;
    private void Update()
    {
        if (!hasTarget)
        {
            targets = GameObject.FindGameObjectsWithTag(targetTag);
            hasCreated = false;
        }
        bool flag = false;
        foreach(GameObject it in targets)
        {
            flag = flag || (it != null);
        }
        if (flag)
        {
            hasTarget = true;
            if (!hasCreated)
            {
                foreach (GameObject it in targets)
                {
                    GameObject instance = GameObject.Instantiate(targetPrefab, transform);
                    instance.GetComponent<TargetMark>().target = it;
                }
                hasCreated = true;
            }
        }
        else hasTarget = false;
    }
}
