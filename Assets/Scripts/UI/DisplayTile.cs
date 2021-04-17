using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    public string ItemName = null;
    [SerializeField] private Image image;
    public Text Label;
    public bool isInCrafting = true;

    private List<GameObject> items = new List<GameObject>();
    private Text label;
    private string caption;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private void Start()
    {
        foreach (GameObject g in Resources.LoadAll("Items", typeof(GameObject)))
        {
            items.Add(g);
        }
        GameObject item = items.Find(i => i.name == ItemName);
        if (item != null)
        {
            Item item1;
            item1 = item.GetComponent<Item>();
            image.sprite = item1.sprite;
            caption = item1.caption;
            if (isInCrafting)
            {
                CraftingRecipeUI crui = GetComponentInParent<CraftingRecipeUI>();
                label = crui.Label;
            }
            else
            {
                label = Label;
                label.text = caption;
            }
            
        }
        else
        {
            image.sprite = null;
            image.color = disabledColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInCrafting)
        {
            label.text = caption;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInCrafting)
        {
            label.text = "";
        }
    }
}
