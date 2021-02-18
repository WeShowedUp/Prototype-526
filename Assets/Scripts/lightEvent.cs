using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightEvent : MonoBehaviour
{
   

    // Start is called before the first frame update
    public LightRespawn Respawnpoint;
    public light obj;
    private void Update()
    {
        obj.RP = Respawnpoint;
    }



}
