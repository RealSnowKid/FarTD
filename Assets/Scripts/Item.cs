using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string caption = "null";
    public Sprite sprite;

    public GameObject tile;

    public Inventory inventory;
    public Text label;

    private bool pickedUp = false;

    public bool isPlaceable = false;
    public GameObject building;

    public void OnPointerEnter(PointerEventData eventData) {
        label.text = caption;
    }

    public void OnPointerExit(PointerEventData eventData) {
        label.text = "";
    }

    public void PickUp() {
        if(inventory.pickedItem == null) {
            tile.GetComponent<InventoryTile>().PickUp();
            tile = null;
            pickedUp = true;
            inventory.pickedItem = gameObject;
        }
    }

    public void PutDown(GameObject tile) {
        pickedUp = false;
        inventory.pickedItem = null;
        this.tile = tile;
    }

    private void Update() {
        if (pickedUp && inventory.pickedItem == gameObject) {
            transform.position = Input.mousePosition + new Vector3(0f, -30f);
        }
    }
}
