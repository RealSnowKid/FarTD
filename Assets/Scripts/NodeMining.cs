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
    public Ore oreType;
    public List<GameObject> resources = new List<GameObject>();
    public Inventory inv;

    private void Start()
    {
        inv = GameObject.Find("MapGeneration").GetComponent<MapGeneration>().GetInventory().GetComponent<Inventory>();
        resources.Add(Resources.Load("OreIronium") as GameObject);
        resources.Add(Resources.Load("OreZonium") as GameObject);
        resources.Add(Resources.Load("PowderInstabilium") as GameObject);
    }
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
        for (int i = 0; i < miningSpeed; i++)
        {
            AddResourcesToInv(oreType);
        }
        Debug.Log(oreCapacity);
    }

    void AddResourcesToInv(Ore oreType)
    {
        switch (oreType)
        {
            case Ore.Ironium:
                inv.AddItem(resources[0]);
                break;
            case Ore.Zonium:
                inv.AddItem(resources[1]);
                break;
            case Ore.Instabilium:
                inv.AddItem(resources[2]);
                break;
            default:
                Debug.Log("No resource found");
                break;
        }
    }
}
