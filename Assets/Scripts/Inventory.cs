using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    [SerializeField] private GameObject inventoryMenu;
    public GameObject player;

    public GameObject pickedItem = null;

    public List<Item> items = new List<Item>();
    public Transform itemsParent;

    public GameObject grid;
    public Text label;

    public List<GameObject> testItems = new List<GameObject>();

    private void Start() {

        foreach(GameObject item in testItems) {
            AddItem(item);
        }
    }

    void Update() {
        if (Input.GetKeyDown("q")) {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);

            Cursor.lockState = inventoryMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = inventoryMenu.activeSelf;

            player.GetComponent<PlayerControl>().enabled = !inventoryMenu.activeSelf;
        }
    }

    void AddItem(GameObject item) {
        GameObject tile = null;

        for (int i = 0; i < 25; i++) {
            if (grid.transform.GetChild(i).GetComponent<InventoryTile>().item == null) {
                tile = grid.transform.GetChild(i).gameObject;
                break;
            }
        }

        if(tile == null) {
            Debug.LogError("No empty place");
            return;
        }


        GameObject visualItem = Instantiate(item, itemsParent);
        items.Add(visualItem.GetComponent<Item>());

        visualItem.GetComponent<Item>().inventory = this;
        visualItem.GetComponent<Item>().label = label;

        tile.GetComponent<InventoryTile>().item = visualItem;
        visualItem.GetComponent<Item>().tile = tile;

        visualItem.transform.position = tile.transform.position;
    }
}
