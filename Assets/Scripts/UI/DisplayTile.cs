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
            CraftingRecipeUI crui = GetComponentInParent<CraftingRecipeUI>();
            label = crui.Label;
        }
        else
        {
            image.sprite = null;
            image.color = disabledColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        label.text = caption;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        label.text = "";
    }
}
