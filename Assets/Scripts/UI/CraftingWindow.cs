using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : MonoBehaviour
{
	[Header("References")]
	[SerializeField] CraftingRecipeUI recipeUIPrefab;
	[SerializeField] RectTransform recipeUIParent;
	[SerializeField] List<CraftingRecipeUI> craftingRecipeUIs;

	[Header("Public Variables")]
	public Inventory Inventory;
	public List<CraftingRecipe> CraftingRecipes;
	public Text Label;

    private void OnValidate()
	{
		Init();
	}

	private void Start()
	{
		Init();
	}

	private void Init()
	{
		recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: craftingRecipeUIs);
		UpdateCraftingRecipes();
	}

	public void UpdateCraftingRecipes()
	{
		for (int i = 0; i < CraftingRecipes.Count; i++)
		{
			if (craftingRecipeUIs.Count == i)
			{
				craftingRecipeUIs.Add(Instantiate(recipeUIPrefab, recipeUIParent, false));
			}
			else if (craftingRecipeUIs[i] == null)
			{
				craftingRecipeUIs[i] = Instantiate(recipeUIPrefab, recipeUIParent, false);
			}

			craftingRecipeUIs[i].Inventory = Inventory;
			craftingRecipeUIs[i].CraftingRecipe = CraftingRecipes[i];
			craftingRecipeUIs[i].Label = Label;
		}

		for (int i = CraftingRecipes.Count; i < craftingRecipeUIs.Count; i++)
		{
			craftingRecipeUIs[i].CraftingRecipe = null;
		}
	}
}
