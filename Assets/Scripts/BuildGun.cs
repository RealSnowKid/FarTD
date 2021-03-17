using System;
using UnityEngine;

public class BuildGun : MonoBehaviour {
    private float distance = 5f;
    public GameObject buildObject = null;

    public bool gunEnabled = false;
    private GameObject instance = null;

    private RaycastHit hitInfo;

    public InventoryTile gunTile = null;

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
        tile.GetComponent<Tile>().building = instance;
        instance.GetComponent<Collider>().enabled = true;
        instance = null;

        Destroy(gunTile.item);
    }

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
                        if (Input.GetButtonDown("Fire1")) Build(hitInfo.collider.gameObject);
                    }
                }catch(NullReferenceException e) {
                    // if we look at a non-tile object
                    Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
                }
            }
        }
    }

}
