using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int ButtonId;
    private ButtonsGroupController buttonsGroupController;

    void Start()
    {
        buttonsGroupController = transform.parent.GetComponent<ButtonsGroupController>(); ;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonsGroupController.NotifySelection(ButtonId);

    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        buttonsGroupController.NotifyHovering(ButtonId);

    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        buttonsGroupController.NotifyStoppedHovering(ButtonId);  
    }
}
