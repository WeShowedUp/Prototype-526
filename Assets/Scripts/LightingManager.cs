using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightingManager : MonoBehaviour
{
    
    public Tilemap DarkMap;
    public Tilemap BlurredMap;
    public Tilemap BaseMap;

    public Tile DarkTile;
    public Tile BlurredTile;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //makes the two lighting maps the same size and space as the base map
        DarkMap.origin = BlurredMap.origin = BaseMap.origin;
        DarkMap.size = BlurredMap.size = BaseMap.size;

        //Fill the lighting maps with their respective tiles
        foreach (Vector3Int p in DarkMap.cellBounds.allPositionsWithin)
        {
            DarkMap.SetTile(p, DarkTile);
        }
        foreach (Vector3Int p in BlurredMap.cellBounds.allPositionsWithin)
        {
            BlurredMap.SetTile(p, BlurredTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
