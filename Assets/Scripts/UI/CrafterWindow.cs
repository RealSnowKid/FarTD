using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterWindow : MonoBehaviour
{
    public void NotifyButtonUpdate(List<bool> activeButtons)
    {
        foreach (var activeButton in activeButtons)
        {
            Debug.Log(activeButton);
        }
    }

    private void SetRecipe()
    {

    }
}
