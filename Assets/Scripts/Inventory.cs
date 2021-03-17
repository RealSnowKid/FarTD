using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private GameObject inventoryMenu;
    public GameObject player;

    public GameObject pickedItem = null;

    void Update() {
        if (Input.GetKeyDown("q")) {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);

            Cursor.lockState = inventoryMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = inventoryMenu.activeSelf;

            player.GetComponent<PlayerControl>().enabled = !inventoryMenu.activeSelf;
        }
    }
}
