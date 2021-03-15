using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform parent;

    public int mapX, mapY;
    private float tileSize;

    // thresholds
    private const float oreThreshold = 0.21f;
    private const float wallThreshold = 0.335f;

    void Start() {

        // texture settings
        float seed = Random.Range(0f, 1000f);
        float scale = mapX/10;

        tileSize = tilePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.x;

        // generate tiles
        for (int i=0; i<mapX; i++) {
            for(int ii=0; ii<mapY; ii++) {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i * tileSize, 0f, ii * tileSize), Quaternion.identity, parent);

                float wallSample = Mathf.PerlinNoise((i + seed) / mapX * scale, (ii + seed) / mapY * scale);

                // generate walls
                if(wallSample < wallThreshold) {
                    tile.transform.localScale = new Vector3(1f, 10f, 1f);
                } else {
                    // generate ores from tiles which are not walls
                    float sample = Mathf.PerlinNoise((i + seed * seed) / mapX * scale, (ii + seed * seed) / mapY * scale);
                    tile.GetComponent<Renderer>().material.color = sample < oreThreshold ? Color.yellow : Color.white;
                }
            }
        }

    }
}
