using UnityEngine;
using UnityEngine.UI;

public class InventoryTile : MonoBehaviour {
    public GameObject item = null;

    public Inventory inventory;

    public void OnClick() {
        // if slot is empty
        if(item == null) {
            // if user is putting item down
            if (inventory.pickedItem != null) {
                item = inventory.pickedItem;
                item.transform.position = gameObject.transform.position;
                item.GetComponent<Item>().PutDown(gameObject);
            }
        }
    }

    public void PickUp() {
        item = null;
    }
}
