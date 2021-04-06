using UnityEngine;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour {
    public GameObject item = null;

    public Inventory inventory;

    public bool isGunTile = false;

    public GameObject player;

    // refactor pls
    void Start() {
        if (isGunTile) {
            player = GameObject.Find("Player(Clone)");
            player.GetComponent<GunSwitcher>().build.gunTile = gameObject;
        }
    }

    public void OnClick() {
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
                    }
                }
            }
        }
    }

    public void PickUp() {
        item = null;
    }
}
