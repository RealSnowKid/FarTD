using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject inventoryMenu;
    public GameObject craftingMenu;
    public GameObject player;

    public GameObject pickedItem = null;

    public List<Item> items = new List<Item>();
    public Transform itemsParent;

    public GameObject grid;
    public Text label;

    public List<GameObject> testItems = new List<GameObject>();
    [SerializeField] private GunSwitcher gunSwitcher;

    private void Start() {
        foreach(GameObject item in testItems) {
            AddItem(item);
        }

        // temp
        gunSwitcher = GameObject.Find("Player(Clone)").GetComponent<GunSwitcher>();
    }

    [SerializeField] GameObject smelteryGUI;

    public GameObject GetSmelteryGUI() {
        return smelteryGUI;
    }

    void Update() {
        if (Input.GetKeyDown("q") && pickedItem == null) {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            craftingMenu.SetActive(inventoryMenu.activeSelf);

            if (inventoryMenu.activeSelf)
                gunSwitcher.OpenedInvenory();
            else
                gunSwitcher.ClosedInventory();

            Cursor.lockState = inventoryMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = inventoryMenu.activeSelf;

            player.GetComponent<PlayerControl>().enabled = !inventoryMenu.activeSelf;
        }
    }

    public bool AddItem(GameObject item) {
        GameObject tile = HasSpace();

        if (tile == null)
        {
            Debug.LogError("No empty place");
            return false;
        }

        GameObject visualItem = Instantiate(item, itemsParent);
        items.Add(visualItem.GetComponent<Item>());

        visualItem.GetComponent<Item>().inventory = this;
        visualItem.GetComponent<Item>().label = label;

        tile.GetComponent<InventoryTile>().item = visualItem;
        visualItem.GetComponent<Item>().tile = tile;

        visualItem.transform.position = tile.transform.position;
        return true;
    }

    public GameObject HasSpace()
    {
        GameObject tile = null;

        for (int i = 0; i < 25; i++)
        {
            if (grid.transform.GetChild(i).GetComponent<InventoryTile>().item == null)
            {
                tile = grid.transform.GetChild(i).gameObject;
                break;
            }
        }
        return tile;
    }

    public bool RemoveItem(GameObject item)
    {
        foreach (Item i in items)
        {
            if (i.caption == item.GetComponent<Item>().caption)
            {
                Destroy(i.gameObject);
                items.Remove(i);
                return true;
            }
        }
        return false;
    }

    public int ItemCount(GameObject item)
    {
        int number = 0;

        foreach (Item i in items)
        {
            if (i.caption == item.GetComponent<Item>().caption)
            {
                number++;
            }
        }
        return number;
    }
}
