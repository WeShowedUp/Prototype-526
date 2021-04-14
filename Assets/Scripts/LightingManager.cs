using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightingManager : MonoBehaviour
{
    //creates a place to link the tiles and tile maps to the code
    public Tilemap DarkMap;
    public Tilemap BlurredMap;
    public Tilemap BaseMap;
    public Tilemap BoarderMap;
    public Tile DarkTile;
    public Tile BlurredTile;
    
    //creates a place to link the player light mask to this code
    public GameObject lightMask;
    public GameObject numberChange;

    float lightPoints;
    //grows and shrinks players light mask based on how many light points they have
    private void adjustFOV()
    {
        
        const float MAX_LIGHT_POINTS = 900; //lightpoints above this will not affect FOV size
        const float MAX_LIGHT_SCALE = 1.5f; //largest the light mask will be scaled
        const float MIN_LIGHT_SCALE = 0.5f; //smallest the light mask will be scaled

        //asks the changing number file for the current number of light points
        lightPoints = numberChange.GetComponent<ChangingNumber>().getLightPts();
        if (lightPoints <0) {lightPoints=0;} //prevents negative values
        if (lightPoints > MAX_LIGHT_POINTS) {lightPoints=MAX_LIGHT_POINTS;}

        //calculate light scale based on number of points
        float lightScaleValue = (lightPoints)*(MAX_LIGHT_SCALE - MIN_LIGHT_SCALE) / MAX_LIGHT_POINTS + MIN_LIGHT_SCALE;
        Vector3 lightScale = new Vector3(lightScaleValue, lightScaleValue, 1);
        //sets light mask around player to new scale
        lightMask.transform.localScale = lightScale;

    }


    // Start is called before the first frame update
    void Start()
    {
        

        //makes the two lighting maps the same size and space as the base map
        DarkMap.origin = BlurredMap.origin = BaseMap.origin = BoarderMap.origin;
        DarkMap.size = BlurredMap.size = BaseMap.size = BoarderMap.size;

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
        adjustFOV();
    }
}