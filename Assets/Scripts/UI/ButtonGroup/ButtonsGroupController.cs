using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonStatus
{
    Idle,
    Hover,
    Active
}

public class ButtonsGroupController : MonoBehaviour
{
    [Header("Single Or Multiple Active")]
    public bool bIsExclusive = true;
    public bool bEnforceOneActive = false;
    public bool bFirstButtonActive = false;

    [Header("Buttons Colors")]
    public Color IdleColor;
    public Color ActiveColor;
    public Color HoverColor;

    private List<GameObject> buttons = new List<GameObject>();
    private List<ButtonStatus> statuses = new List<ButtonStatus>();
    private int numberButtons;

    public CrafterWindow crafterWindow;

    void Start()
    {
        RegisterElements();
        crafterWindow.NotifyButtonUpdate(GetListActiveButtons());
    }

    public void RegisterElements()
    {
        numberButtons = transform.childCount;
        for (int i = 0; i < numberButtons; i++)
        {
            GameObject currChild = transform.GetChild(i).gameObject;
            currChild.GetComponent<ClickableButton>().buttonId = i;
            buttons.Add(currChild);

            statuses.Add(ButtonStatus.Idle);
        }

        statuses[0] = (bFirstButtonActive) ? ButtonStatus.Active : ButtonStatus.Idle;
        if (bEnforceOneActive)
        {
            statuses[0] = ButtonStatus.Active;
        }

        SetColorButtons();

    }

    private void SetColorButtons()
    {
        for (int i = 0; i < numberButtons; i++)
        {
            Color buttonColor = IdleColor;
            if (statuses[i] == ButtonStatus.Active)
            {
                buttonColor = ActiveColor;
            }
            else if (statuses[i] == ButtonStatus.Hover)
            {
                buttonColor = HoverColor;
            }
            buttons[i].GetComponent<Image>().color = buttonColor;
        }
    }

    public void NotifySelection(int idButton)
    {
        bool isDirty = false;

        if (statuses[idButton] == ButtonStatus.Active)
        {
            if (bEnforceOneActive)
            {
                if (GetNumberActive() > 1)
                {
                    isDirty = true;
                    statuses[idButton] = ButtonStatus.Idle;

                }
            } 
            else
            {
                isDirty = true;
                statuses[idButton] = ButtonStatus.Idle;
            }

        } 
        else
        {
            if (bIsExclusive)
            {
                for (int i = 0; i < numberButtons; i++)
                {
                    statuses[i] = ButtonStatus.Idle;
                }
            }
            statuses[idButton] = ButtonStatus.Active;
            isDirty = true;
        }

        if (isDirty)
        {
            SetColorButtons();
        }

        crafterWindow.NotifyButtonUpdate(GetListActiveButtons());
    }

    public void NotifyHovering(int idButton)
    {
        if (statuses[idButton] != ButtonStatus.Hover &&statuses[idButton] != ButtonStatus.Active)
        {
            statuses[idButton] = ButtonStatus.Hover;
            SetColorButtons();
        }
    }

    public void NotifyStoppedHovering(int idButton)
    {
        if (statuses[idButton] != ButtonStatus.Active)
        {
            statuses[idButton] = ButtonStatus.Idle;
            SetColorButtons();
        }
    }

    public List<bool> GetListActiveButtons()
    {
        
        List<bool> activeButtons = new List<bool>(buttons.Count);
        for (int i = 0; i < numberButtons; i++)
        {
            activeButtons.Add(statuses[i] == ButtonStatus.Active);
        }

        return activeButtons;
    }

    public int GetNumberActive()
    {
        int nbActive = 0;
        foreach (ButtonStatus status in statuses)
        {
            if (status == ButtonStatus.Active)
            {
                nbActive++;
            }
        }

        return nbActive;
    }
}
