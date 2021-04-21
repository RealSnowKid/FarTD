using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public Item Item;
    [Range(1, 999)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(Inventory inventory)
    {
        if (inventory.HasSpace() != null & HasMaterials(inventory))
        {
            return true;
        }
        return false;
    }

    private bool HasMaterials(Inventory inventory)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if (inventory.ItemCount(itemAmount.Item.gameObject) < itemAmount.Amount)
            {
                Debug.LogWarning("You don't have the required materials.");
                return false;
            }
        }
        return true;
    }

    public void Craft(Inventory inventory)
    {
        if (CanCraft(inventory))
        {
            RemoveMaterials(inventory);
            AddResults(inventory);
        }
    }

    private void RemoveMaterials(Inventory inventory)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                inventory.RemoveItem(itemAmount.Item.gameObject);
            }
        }
    }

    private void AddResults(Inventory inventory)
    {
        foreach (ItemAmount itemAmount in Results)
        {
            for (int i = 0; i < itemAmount.Amount; i++)
            {
                inventory.AddItem(itemAmount.Item.gameObject);
            }
        }
    }
}
