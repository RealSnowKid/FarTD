using System;
using UnityEngine;

public class BuildGun : MonoBehaviour {
    private float distance = 5f;
    public GameObject buildObject = null;

    public bool gunEnabled = false;
    private GameObject instance = null;

    private RaycastHit hitInfo;

    public GameObject gunTile = null;

    private GameObject buildParent;

    // refactorr
    private void Start() {
        buildParent = GameObject.Find("Buildings");
    }

    public void Enable() {
        gunEnabled = true;
    }

    public void Disable() {
        gunEnabled = false;
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
        instance.GetComponent<Collider>().enabled = true;
        instance = null;

        Destroy(gunTile.GetComponent<InventoryTile>().item);
    }

    // TODO: interactions while not looking at the tile should also work
    void Update() {
        // if gun is aenabled are a building is selected
        if (gunEnabled && instance != null) {
            // if we look at a tile
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance)) {
                try {
                    // if the tile is a ground tile and it's empty
                    if (!hitInfo.collider.gameObject.GetComponent<Tile>().isWall && hitInfo.collider.gameObject.GetComponent<Tile>().building == null) {
                        Debug.DrawRay(transform.position, transform.forward * distance, Color.yellow);

                        // move the hologram to the desegnated tile
                        instance.transform.position = hitInfo.collider.gameObject.transform.position;

                        // if LMB is clicked, build it
                        if (Input.GetButtonDown("Fire1")) {
                            // TODO: maybe put all these in a separate class
                            // if we're building a miner
                            if(instance.GetComponent<Miner>() != null) {
                                // if ore is on the tile
                                if(hitInfo.collider.gameObject.GetComponent<Tile>().oreType != Ore.None) {
                                    instance.GetComponent<Miner>().setOre(hitInfo.collider.gameObject.GetComponent<Tile>().oreType);
                                    Build(hitInfo.collider.gameObject);
                                } else {
                                    Debug.LogError("Miners must be placed on an ore tile");
                                }
                            }
                            //if we're building a conveyor
                            if(instance.GetComponent<ConveyorMovement>() != null) {
                                Build(hitInfo.collider.gameObject);
                            }
                        }

                        // if "r" is pressed, rotate the object
                        if (Input.GetKeyDown("r")) {
                            instance.transform.Rotate(new Vector3(0f, 90f, 0f));
                        }
                    }
                }catch(NullReferenceException) {
                    // if we look at a non-tile object
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
                }
            }
        }
    }

}
