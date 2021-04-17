using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int buttonId;
    private ButtonsGroupController buttonsGroupController;

    void Start()
    {
        buttonsGroupController = transform.parent.GetComponent<ButtonsGroupController>(); ;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonsGroupController.NotifySelection(buttonId);

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        buttonsGroupController.NotifyHovering(buttonId);

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        buttonsGroupController.NotifyStoppedHovering(buttonId);  
    }
}
