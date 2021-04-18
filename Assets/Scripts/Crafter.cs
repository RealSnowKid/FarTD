using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafter : Building
{
    public CraftingRecipe CraftingRecipe;
    public GameObject Craftable;
    private List<GameObject> items = new List<GameObject>();
    public Transform SpawnLocation;
    private Transform spawnParent;
    public bool isBuilt = false;
    private bool crafting = false;

    // Debugging purposes
    public string Name;
    private Transform buildings;

    // Bit of a WeridChamp
    private float speed = 2f;
    private GameObject gObject;

    private void Start()
    {
        spawnParent = GameObject.Find("ObjectsSpawned").transform;
        buildings = GameObject.Find("Buildings").transform;
        CraftingRecipe = gameObject.transform.GetComponentInParent<CrafterUITrigger>().CraftingRecipes[1];
        Craftable = gameObject.transform.GetComponentInParent<CrafterUITrigger>().Spawnables[0];
    }

    public void Build()
    {
        isBuilt = true;
        // Debugging purposes
        int number = 0;
        for (int i = 0; i < buildings.childCount; i++)
        {
            if (buildings.GetChild(i).name == this.transform.parent.name)
            {
                number++;
            }
        }
        Name = "Crafter " + number;
    }

    public void SetRecipe(CraftingRecipe craftingRecipe, GameObject craftable)
    {
        this.CraftingRecipe = craftingRecipe;
        this.Craftable = craftable;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<OreDrop>() != null && CraftingRecipe == null && isBuilt == true)
        {
            Destroy(collider.gameObject);
        }
        else if(collider.GetComponent<OreDrop>() != null && CraftingRecipe != null && isBuilt == true && crafting == false)
        {
            bool included = false;
            if (collider.GetComponent<OreDrop>().Item != null)
            {
                foreach (ItemAmount ia in CraftingRecipe.Materials)
                {
                    if (ia.Item == collider.GetComponent<OreDrop>().Item)
                    {
                        included = true;
                        items.Add(collider.GetComponent<OreDrop>().gameObject);
                        break;
                    }
                }
                if (included == false)
                {
                    Destroy(collider.gameObject);
                }
            }
            else
            {
                Destroy(collider.gameObject);
            }
        }
        else if(collider.GetComponent<OreDrop>() != null && CraftingRecipe != null && isBuilt == true && crafting == true)
        {
            Destroy(collider.gameObject);
        }
        if (CountItems())
        {
            Craft();
        }
    }

    private bool CountItems()
    {
        int allHere = 0;
        foreach (var itemAmount in CraftingRecipe.Materials)
        {
            int count = items.Where(item => item.GetComponent<OreDrop>().Item == itemAmount.Item).Count();
            if (count == itemAmount.Amount)
            {
                allHere++;
            }
        }
        if (allHere == CraftingRecipe.Materials.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Craft()
    {
        crafting = true;
        StartCoroutine("Crafting");
    }

    IEnumerator Crafting()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject itemToSpawn = Craftable;
        crafting = false;
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
        items.Clear();
        GameObject spawnedItem = Instantiate(itemToSpawn, SpawnLocation.position, SpawnLocation.rotation, spawnParent);
        gObject = spawnedItem;
        InvokeRepeating("PushForward", 0, 0.02f);
        StartCoroutine("CancelPushInvoke");
    }

    IEnumerator CancelPushInvoke()
    {
        yield return new WaitForSeconds(0.6f);
        CancelInvoke("PushForward");
    }

    private void PushForward()
    {
        gObject.transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
    }
}
