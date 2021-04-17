using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterUITrigger : MonoBehaviour
{
    private GameObject crosshair;
    [SerializeField] private GameObject crafterUI;
    private GameObject crafter;
    private GameObject inventory;
    private GameObject player;
    private bool menuOpen = false;

    private void Start()
    {
        crafter = gameObject.transform.GetChild(0).gameObject;
        crafterUI = GameObject.Find("Canvas").GetComponent<Inventory>().CrafterUI.gameObject;
        crosshair = GameObject.Find("Crosshair");
        player = GameObject.Find("Canvas").GetComponent<Inventory>().player;
        inventory = GameObject.Find("Canvas").GetComponent<Inventory>().inventoryMenu;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent<PlayerControl>() != null && crafter.GetComponent<Crafter>().isBuilt == true)
        {
            if (Input.GetKeyDown("e"))
            {
                menuOpen = !menuOpen;
                HandleUIWindow();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<PlayerControl>() != null && crafter.GetComponent<Crafter>().isBuilt == true)
        {
            Debug.Log("Player Exited Area");
            menuOpen = false;
            HandleUIWindow();
        }
    }

    private void HandleUIWindow()
    {
        if (crafter.GetComponent<Crafter>().isBuilt == true)
        {
            if (menuOpen)
            {
                crafterUI.SetActive(true);
                crosshair.SetActive(false);
            }
            else if (!menuOpen)
            {
                crafterUI.SetActive(false);
                crosshair.SetActive(true);
            }
            if (!inventory.activeSelf)
            {
                Cursor.lockState = crafterUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = crafterUI.activeSelf;

                player.GetComponent<PlayerControl>().enabled = !crafterUI.activeSelf;
            }
        }
    }
}
