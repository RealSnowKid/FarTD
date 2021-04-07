using UnityEngine;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour {
    public GameObject item = null;

    public Inventory inventory;

    public bool isGunTile = false;

    public bool isInputTile = false;
    public bool isFuelTile = false;
    public bool isOutputTile = false;

    public GameObject player;

    // refactor pls
    void Start() {
        if (isGunTile || isInputTile || isFuelTile || isOutputTile) {
            player = GameObject.Find("Player(Clone)");
            if (isGunTile) player.GetComponent<GunSwitcher>().build.gunTile = gameObject;
        }
    }

    public void OnClick() {
        // if its the output box you should be able to put anything there
        if (isOutputTile) return;
        if (isInputTile || isFuelTile) {

        }

        // if slot is empty
        if(item == null) {
            // if user is putting item down
            if (inventory.pickedItem != null) {
                if (isGunTile && !inventory.pickedItem.GetComponent<Item>().isPlaceable) {
                    Debug.LogWarning("Only placeable item can be put there");
                } else {
                    item = inventory.pickedItem;
                    item.transform.position = gameObject.transform.position;
                    item.GetComponent<Item>().PutDown(gameObject);

                    if (isGunTile) {
                        player.GetComponent<GunSwitcher>().UpdateBuildGun(item.GetComponent<Item>().building);
                    } else if (isInputTile || isFuelTile) {
                        GameObject smelter = inventory.player.GetComponent<ClosestSmelter>().smelter;
                        if(smelter == null) {
                            Debug.LogError("No smelter was saved");
                            return;
                        }

                        if (isInputTile) {
                            smelter.GetComponent<Smelter>().input = item;
                        } else if (isFuelTile) {
                            smelter.GetComponent<Smelter>().fuel = item;
                        }

                        item.GetComponent<Item>().PutDown(gameObject);

                    }
                }
            }
        }
    }

    public void PickUp() {
        if (isInputTile)
            player.GetComponent<ClosestSmelter>().smelter.GetComponent<Smelter>().input = null;
        else if(isFuelTile)
            player.GetComponent<ClosestSmelter>().smelter.GetComponent<Smelter>().fuel = null;
        else if (isOutputTile)
            player.GetComponent<ClosestSmelter>().smelter.GetComponent<Smelter>().output = null;

        item = null;
    }
}
