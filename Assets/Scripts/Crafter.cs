using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : Building
{
    private bool isBuilt = false;
    
    public void Build()
    {
        isBuilt = true;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<OreDrop>() != null)
        {
            collider.gameObject.transform.Translate(new Vector3(0, 0, 0), Space.World);
        }
    }
}
