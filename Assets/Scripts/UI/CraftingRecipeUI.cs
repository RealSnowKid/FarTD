using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingRecipeUI : MonoBehaviour
{
	[Header("References")]
	[SerializeField] RectTransform arrowParent;
	[SerializeField] DisplayTile[] itemSlots;
	public Text Label;

	[Header("Inventory")]
	public Inventory Inventory;

	private CraftingRecipe craftingRecipe;
	public CraftingRecipe CraftingRecipe
	{
		get { return craftingRecipe; }
		set { SetCraftingRecipe(value); }
	}

	private void OnValidate()
	{
		itemSlots = GetComponentsInChildren<DisplayTile>(includeInactive: true);
	}

	public void OnCraftButtonClick()
	{
		if (craftingRecipe != null && Inventory != null)
		{
			craftingRecipe.Craft(Inventory);
		}
	}

	private void SetCraftingRecipe(CraftingRecipe newCraftingRecipe)
	{
		craftingRecipe = newCraftingRecipe;

		if (craftingRecipe != null)
		{
			int slotIndex = 0;
			slotIndex = SetSlots(craftingRecipe.Materials, slotIndex);
			arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(craftingRecipe.Results, slotIndex);

            for (int i = slotIndex; i < itemSlots.Length; i++)
			{
				itemSlots[i].transform.parent.gameObject.SetActive(false);
			}

			gameObject.SetActive(true);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
	{
        if (itemAmountList.Count == 1 && itemAmountList[0].Amount == 2)
        {
			ItemAmount itemAmount = itemAmountList[0];
			DisplayTile itemSlot = itemSlots[0];
			itemSlot.ItemName = itemAmount.Item.name;
			slotIndex++;
			DisplayTile itemSlot2 = itemSlots[1];
			itemSlot2.ItemName = itemAmount.Item.name;
            itemSlot2.transform.parent.gameObject.SetActive(true);
			slotIndex++;
		}
        else
        {
            for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
            {
                ItemAmount itemAmount = itemAmountList[i];
                DisplayTile itemSlot = itemSlots[slotIndex];

                itemSlot.ItemName = itemAmount.Item.name;
                itemSlot.transform.parent.gameObject.SetActive(true);
            }
        }
        return slotIndex;
	}
}
