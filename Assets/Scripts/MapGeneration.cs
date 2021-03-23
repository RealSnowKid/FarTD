using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject oreNodePrefab;
    [SerializeField] private GameObject corePrefab;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject compass;
    [SerializeField] private GameObject inventory;

    [SerializeField] private Transform parent;
    [SerializeField] private Transform oreNodeParent;

    public int mapX, mapY;
    private float tileSize;

    // thresholds
    private const float oreThreshold = 0.21f;
    private const float oreThreshold2 = 0.15f;
    private const float wallThreshold = 0.3f;

    private float ironiumPercent = 40f;
    private float zoniumPercent = 20f;
    private float ventiumPercent = 10f;
    private float memiumPercent = 6f;
    private float unobtaniumPercent = 4f;
    private float instabiliumPercent = 20f;

    void Awake() {
        if(ironiumPercent + zoniumPercent + ventiumPercent + memiumPercent + unobtaniumPercent + instabiliumPercent != 100f) {
            Debug.LogError("Ore percentages not equal to 100!");
            return;
        }

        if(mapX < 40 || mapY < 40) {
            Debug.LogError("Map size must be at least 40x40");
            return;
        }

        zoniumPercent += ironiumPercent;
        ventiumPercent += zoniumPercent;
        memiumPercent += ventiumPercent;
        unobtaniumPercent += memiumPercent;
        instabiliumPercent += unobtaniumPercent;

        // texture settings
        float seed = Random.Range(0f, 1000f);
        float scale = mapX/8;

        tileSize = tilePrefab.transform.localScale.x;

        Dictionary<string, GameObject> ores = new Dictionary<string, GameObject>();
        Dictionary<string, GameObject> oresCluster = new Dictionary<string, GameObject>();

        int playerX, playerY;

        // spawn player
        do {
            playerX = Random.Range(21, mapX - 20);
            playerY = Random.Range(21, mapY - 20);
        } while (Mathf.PerlinNoise((playerX + seed) / mapX * scale, (playerY + seed) / mapY * scale) < wallThreshold);

        // generate tiles
        for (int i=0; i<mapX; i++) {
            for(int ii=0; ii<mapY; ii++) {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i * tileSize, 0f, ii * tileSize), Quaternion.identity, parent);
                tile.name = i.ToString() + "," + ii.ToString();

                float wallSample = Mathf.PerlinNoise((i + seed) / mapX * scale, (ii + seed) / mapY * scale);

                // generate walls
                if(wallSample < wallThreshold) {
                    tile.transform.localScale = new Vector3(tileSize, 10f, tileSize);
                    tile.GetComponent<Tile>().isWall = true;
                } else {
                    //spawn player
                    if(i == playerX && ii == playerY) {
                        GameObject player = Instantiate(playerPrefab, tile.transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
                        GameObject core = Instantiate(corePrefab, tile.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                        tile.GetComponent<Tile>().building = core;

                        compass.GetComponent<Compass>().player = player;
                        compass.GetComponent<Compass>().AddMarker(core.GetComponent<CompassMarker>());

                        inventory.GetComponent<Inventory>().player = player;
                    }

                    // generate ores from tiles which are not walls
                    float oreSample = Mathf.PerlinNoise((i + seed * seed) / mapX * scale, (ii + seed * seed) / mapY * scale);
                    if (oreSample < oreThreshold) {
                        tile.GetComponent<Renderer>().material.color = Color.yellow;

                        ores.Add(tile.name, tile);

                        GameObject adjacentOre = null;

                        // check if part of an ore patch
                        if (ores.ContainsKey((i-1).ToString() + "," + (ii).ToString()))
                            adjacentOre = ores[(i-1).ToString() + "," + (ii).ToString()];
                        else if (ores.ContainsKey((i).ToString() + "," + (ii-1).ToString()))
                            adjacentOre = ores[(i).ToString() + "," + (ii-1).ToString()];
                        else if (ores.ContainsKey((i+1).ToString() + "," + (ii).ToString()))
                            adjacentOre = ores[(i+1).ToString() + "," + (ii).ToString()];
                        else if (ores.ContainsKey((i).ToString() + "," + (ii+1).ToString()))
                            adjacentOre = ores[(i).ToString() + "," + (ii+1).ToString()];
                        else if (ores.ContainsKey((i-1).ToString() + "," + (ii+1).ToString()))
                            adjacentOre = ores[(i-1).ToString() + "," + (ii+1).ToString()];
                        else if (ores.ContainsKey((i+1).ToString() + "," + (ii-1).ToString()))
                            adjacentOre = ores[(i+1).ToString() + "," + (ii-1).ToString()];
                        else if (ores.ContainsKey((i-1).ToString() + "," + (ii-1).ToString()))
                            adjacentOre = ores[(i-1).ToString() + "," + (ii-1).ToString()];
                        else if (ores.ContainsKey((i+1).ToString() + "," + (ii+1).ToString()))
                            adjacentOre = ores[(i+1).ToString() + "," + (ii+1).ToString()];
                        else {

                            // if it's a new ore patch, generate random ore
                            float rnd = Random.Range(0f, 100f);

                            if (rnd <= ironiumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Ironium;
                            } else if (rnd <= zoniumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Zonium;
                            } else if (rnd <= ventiumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Ventium;
                            } else if (rnd <= memiumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Memium;
                            } else if (rnd <= unobtaniumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Unobtanium;
                            } else if (rnd <= instabiliumPercent) {
                                tile.GetComponent<Tile>().oreType = Ore.Instabilium;
                            }
                        }

                        if(adjacentOre != null)
                            tile.GetComponent<Tile>().oreType = adjacentOre.GetComponent<Tile>().oreType;
                            

                        tile.GetComponent<Tile>().UpdateVisuals();
                    }

                    if (oreSample < oreThreshold2)
                    {
                        bool anyAdjacent = false;
                        if (oresCluster.ContainsKey((i - 1).ToString() + "," + (ii).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i).ToString() + "," + (ii - 1).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i + 1).ToString() + "," + (ii).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i).ToString() + "," + (ii + 1).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i - 1).ToString() + "," + (ii + 1).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i + 1).ToString() + "," + (ii - 1).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i - 1).ToString() + "," + (ii - 1).ToString()))
                            anyAdjacent = true;
                        else if (oresCluster.ContainsKey((i + 1).ToString() + "," + (ii + 1).ToString()))
                            anyAdjacent = true;
                        if (anyAdjacent == false)
                        {
                            if (tile.GetComponent<Tile>().oreType == Ore.Ventium || tile.GetComponent<Tile>().oreType == Ore.Memium || tile.GetComponent<Tile>().oreType == Ore.Unobtanium)
                            {

                            }
                            else
                            {
                                oresCluster.Add(tile.name, tile);
                                tile.GetComponent<Tile>().hasOreNode = true;
                                GameObject node = Instantiate(oreNodePrefab, tile.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

                                node.transform.parent = oreNodeParent;
                                node.GetComponent<NodeMining>().oreType = tile.GetComponent<Tile>().oreType;
                                node.transform.GetChild(0).GetComponent<Renderer>().material.color = tile.GetComponent<Renderer>().material.color;
                            }
                            
                        }
                    }
                }
            }
        }
        ores.Clear();
    }

    public GameObject GetInventory()
    {
        return inventory;
    }
}
