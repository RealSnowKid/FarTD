using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : Building
{
    private bool isBuilt = false;
    [SerializeField] CraftingRecipe craftingRecipe;
    private List<Item> items;
    
    public void Build()
    {
        isBuilt = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<OreDrop>() != null && craftingRecipe == null && isBuilt == true)
        {
            Destroy(collider.gameObject);
        }
        else if(collider.GetComponent<OreDrop>() != null && craftingRecipe != null && isBuilt == true)
        {
            bool included = false;
            if (collider.GetComponent<OreDrop>().Item != null)
            {
                Debug.Log("Item: " + collider.GetComponent<OreDrop>().Item);
                foreach (ItemAmount ia in craftingRecipe.Materials)
                {
                    if (ia.Item == collider.GetComponent<OreDrop>().Item)
                    {
                        included = true;
                        items.Add(collider.GetComponent<OreDrop>().Item);
                        Debug.Log("Item added: " + collider.GetComponent<OreDrop>().Item);
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
    }
}
