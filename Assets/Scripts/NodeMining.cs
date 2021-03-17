using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMining : MonoBehaviour
{
    [Tooltip("The total amount of ore in the node")]
    public float oreCapacity = 100f;
    [Tooltip("The amount of ore mined every second")]
    public float miningSpeed = 2f;
    
    private bool mining = false;

    private void Update()
    {
        if (mining == true)
        {
            InvokeRepeating("MineOre", 1, 10);
        }
        if (oreCapacity <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.GetComponent("PlayerControl") != null)
        {
            if (Input.GetKey("e"))
            {
                mining = true;
            }
            else
            {
                mining = false;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent("PlayerControl") != null)
        {
            mining = false;
        }
    }

    void MineOre()
    {
            oreCapacity -= miningSpeed;
            Debug.Log(oreCapacity);
    }
}
