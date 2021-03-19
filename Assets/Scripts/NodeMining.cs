using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class NodeMining : MonoBehaviour
{
    [Tooltip("The total amount of ore in the node")]
    public float oreCapacity = 100f;
    [Tooltip("The amount of ore mined every second")]
    public float miningSpeed = 2f;
    
    private bool mining = false;
    private void Update()
    {
        MineOre();
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
        if (!mining)
        {
            CancelInvoke("ReduceOre");
            return;
        }
        if (!IsInvoking())
        {
            InvokeRepeating("ReduceOre", 0, 1f);   
        }
        return;
    }

    void ReduceOre()
    {
        oreCapacity -= miningSpeed;
        Debug.Log(oreCapacity);
    }
}
