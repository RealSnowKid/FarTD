using System;
using System.Collections;
using UnityEngine;
using System.Threading;
using UnityEngine.AI;

public class BuildGun : MonoBehaviour {
    private float distance = 5f;
    public GameObject buildObject = null;

    public bool gunEnabled = false;
    public GameObject instance = null;

    private RaycastHit hitInfo;

    public GameObject gunTile = null;

    private GameObject buildParent;

    public NavMeshSurface surface;
    public AudioClip buildingSound;

    // refactorr
    private void Start() {
        buildParent = GameObject.Find("Buildings");
    }

    public void Enable() {
        gunEnabled = true;
        if (buildObject != null && instance == null) ChangeBuildObject(buildObject);
    }

    public void Disable() {
        gunEnabled = false;
        lastTile = null;
        Destroy(instance);
    }

    public void ChangeBuildObject(GameObject obj) {
        if (instance != null) Destroy(instance);
        buildObject = obj;
        instance = Instantiate(buildObject);
        instance.GetComponent<Collider>().enabled = false;
    }

    void Build(GameObject tile) {
        instance.transform.parent = buildParent.transform;
        tile.GetComponent<Tile>().building = instance;
        AudioSource.PlayClipAtPoint(buildingSound, tile.transform.position);
        instance.GetComponent<Collider>().enabled = true;
        if (instance.GetComponent<Miner>() != null)
        {
            instance.GetComponent<Miner>().Build();
        }
        instance = null;

        Destroy(gunTile.GetComponent<InventoryTile>().item);

        buildObject = null;
        lastTile = null;
    }

    IEnumerator UpdateNavMesh() {
        surface.UpdateNavMesh(surface.navMeshData);
        //surface.BuildNavMesh();
        yield break;
    }

    GameObject lastTile = null;

    void Update() {
        // if gun is enabled and a building is selected
        if (gunEnabled && instance != null) {

            // if we look at a tile
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance)) {
                try {
                    // if the tile is a wall or has an ore node
                    if (hitInfo.collider.gameObject.GetComponent<Tile>().isWall || hitInfo.collider.gameObject.GetComponent<Tile>().hasOreNode) {
                        throw new NullReferenceException();
                    }
                    // if the tile is a ground tile and it's empty
                    if (hitInfo.collider.gameObject.GetComponent<Tile>().building == null) {
                        Debug.DrawRay(transform.position, transform.forward * distance, Color.yellow);
                        lastTile = hitInfo.collider.gameObject;

                        // move the hologram to the designated tile
                        instance.transform.position = lastTile.transform.position;
                    }
                } catch (NullReferenceException) {
                    // if we look at a non-ground object
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
                }
            }

            // if "r" is pressed, rotate the object
            if (Input.GetKeyDown("r") && lastTile != null)
                instance.transform.Rotate(new Vector3(0f, 90f, 0f), Space.World);

            // if LMB is clicked, build it
            if (Input.GetButtonDown("Fire1") && lastTile != null) {
                // TODO: maybe put all these in a separate class
                // if we're building a miner
                if (instance.GetComponent<Miner>() != null) {
                    // if ore is on the tile
                    if (lastTile.GetComponent<Tile>().oreType != Ore.None) {
                        instance.GetComponent<Miner>().setOre(lastTile.GetComponent<Tile>().oreType);
                        Build(lastTile);
                    } else {
                        Debug.LogWarning("Miners must be placed on an ore tile");
                    }
                }
                //if we're building a conveyor
                else if (instance.GetComponent<ConveyorMovement>() != null) {

                    //if conveyor smelter
                    if(instance.transform.GetChild(0).GetComponent<Smelter>() != null) {
                        instance.transform.GetChild(0).GetComponent<Smelter>().Build();
                    }
                    Build(lastTile);
                }
                //if we're building a smelter
                else if(instance.GetComponent<Smelter>() != null) {
                    instance.GetComponent<Smelter>().Build();
                    Build(lastTile);
                }
                //if we're building a wall
                else if(instance.GetComponent<Wall>() != null) {
                    instance.GetComponent<Wall>().SetSurface(surface);
                    Build(lastTile);
                    StartCoroutine(UpdateNavMesh());
                }else if(instance.GetComponent<Turret>() != null) {
                    instance.GetComponent<Turret>().Build();
                    Build(lastTile);
                }else if(instance.GetComponent<Building>() != null) {
                    Build(lastTile);
                }
                else if(instance.transform.GetChild(0).GetComponent<Crafter>() != null)
                {
                    instance.transform.GetChild(0).GetComponent<Crafter>().Build();
                    Build(lastTile);
                }
            }
        }
    }
}
