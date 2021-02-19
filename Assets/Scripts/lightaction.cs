using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightaction : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Respawnpoint;

    

    public void setRespawnPoint(GameObject point)
    {
        Respawnpoint = point;
    }
    public GameObject getRespawnPoint()
    {
        return Respawnpoint;
    }
    void Count()
    {
        Respawnpoint.GetComponent<LightRespawn>().LightCount--;
    }
}
