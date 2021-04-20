using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterUITrigger : MonoBehaviour
{
    public List<CraftingRecipe> CraftingRecipes = new List<CraftingRecipe>();
    public List<GameObject> Spawnables = new List<GameObject>();

    private GameObject crosshair;
    private GameObject crafterUI;
    private GameObject crafterUINotification;
    private GameObject crafter;
    private GameObject inventory;
    private GameObject player;
    private bool menuOpen = false;

    private void Start()
    {
        crafter = gameObject.transform.GetChild(0).gameObject;
        crafterUI = GameObject.Find("GUI").GetComponent<Inventory>().CrafterUI.gameObject;
        crosshair = GameObject.Find("Crosshair");
        crafterUINotification = GameObject.Find("GUI").GetComponent<Inventory>().CrafterUINotification;
        player = GameObject.Find("GUI").GetComponent<Inventory>().player;
        inventory = GameObject.Find("GUI").GetComponent<Inventory>().inventoryMenu;
        crafterUI.GetComponent<CrafterWindow>().AddButtons();
        crafterUI.GetComponent<CrafterWindow>().SetRecipeImages(CraftingRecipes);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerControl>() != null && crafter.GetComponent<Crafter>().isBuilt == true)
        {
            HandleUIWindow();
        }
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
            menuOpen = false;
            HandleUIWindow();
            crafterUINotification.SetActive(false);
        }
    }

    private void HandleUIWindow()
    {
        if (crafter.GetComponent<Crafter>().isBuilt == true)
        {
            if (menuOpen)
            {
                crafterUI.GetComponent<CrafterWindow>().SetCrafter(crafter);
                crafterUI.SetActive(true);
                crosshair.SetActive(false);
                crafterUINotification.SetActive(false);
            }
            else if (!menuOpen)
            {
                crafterUI.SetActive(false);
                crafterUI.GetComponent<CrafterWindow>().SetCrafter(crafter);
                crosshair.SetActive(true);
                crafterUINotification.SetActive(true);
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
