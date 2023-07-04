using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed = 0.1f;
    void Start()
    {
     


    }

    // Update is called once per frame
    void Update()
    {
        float displacement = Time.time * scrollSpeed;
        TerrainLayer[] tlayers = Terrain.activeTerrain.terrainData.terrainLayers;
        tlayers[2].tileOffset =  new Vector2(displacement, displacement);
        tlayers[1].tileOffset = new Vector2(displacement, displacement);

    }
}
