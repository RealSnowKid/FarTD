using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrafterWindow : MonoBehaviour
{
    [SerializeField] private Crafter crafter;
    [SerializeField] private ButtonsGroupController buttonsGroupController;
    [SerializeField] private List<DisplayTile> buttonDisplayTiles = new List<DisplayTile>();
    [SerializeField] private List<DisplayTile> inputsAndOutput = new List<DisplayTile>();

    public void NotifyButtonUpdate(List<bool> activeButtons)
    {
        if (crafter != null)
        {
            List<CraftingRecipe> craftingRecipes = crafter.GetComponentInParent<CrafterUITrigger>().CraftingRecipes;
            List<GameObject> spawnables = crafter.GetComponentInParent<CrafterUITrigger>().Spawnables;
            for (int i = 0; i < activeButtons.Count; i++)
            {
                if (activeButtons[i] == true)
                {
                    crafter.CraftingRecipe = craftingRecipes[i];
                    crafter.Craftable = spawnables[i];
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                }
            }
        }
    }

    public void SetCrafter(GameObject crafter)
    {
        if (crafter.GetComponent<Crafter>() != null)
        {
            this.crafter = crafter.GetComponent<Crafter>();
            UpdateCrafterName();
        }
        else
        {
            this.crafter = null;
            UpdateCrafterName();
        }
    }

    public void SetRecipeImages(List<CraftingRecipe> craftingRecipes)
    {
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            buttonDisplayTiles[i].ItemName = craftingRecipes[i].Results[0].Item.name;
        }
    }

    public void SetActiveRecipe()
    {
        if (crafter != null)
        {
            switch (crafter.CraftingRecipe.Results[0].Item.name)
            {
                case "IroniumRod":
                    buttonsGroupController.NotifySelection(0);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                case "Miner":
                    buttonsGroupController.NotifySelection(1);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                case "Conveyor":
                    buttonsGroupController.NotifySelection(2);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                case "Smelter":
                    buttonsGroupController.NotifySelection(3);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                case "C_Smelter":
                    buttonsGroupController.NotifySelection(4);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                case "Bullet":
                    buttonsGroupController.NotifySelection(5);
                    SetInputOutputImages(crafter.CraftingRecipe);
                    break;
                default:
                    Debug.LogError("Something went wrong with getting the recipe from the crafter");
                    break;
            }
        }
    }

    private void SetInputOutputImages(CraftingRecipe craftingRecipe)
    {
        if (craftingRecipe.Materials.Count == 1 && craftingRecipe.Materials[0].Amount == 2)
        {
            inputsAndOutput[0].ItemName = craftingRecipe.Materials[0].Item.name;
            inputsAndOutput[1].ItemName = craftingRecipe.Materials[0].Item.name;
            inputsAndOutput[2].ItemName = craftingRecipe.Results[0].Item.name;
        }
        else
        {
            inputsAndOutput[0].ItemName = craftingRecipe.Materials[0].Item.name;
            inputsAndOutput[1].ItemName = craftingRecipe.Materials[1].Item.name;
            inputsAndOutput[2].ItemName = craftingRecipe.Results[0].Item.name;
        }
    }

    // Debugging purposes
    private void UpdateCrafterName()
    {
        if (crafter != null)
        {
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = crafter.Name;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = "Null";
        }
    }
}
