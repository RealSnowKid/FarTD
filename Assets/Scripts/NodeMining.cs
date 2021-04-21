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
    private List<GameObject> resources = new List<GameObject>();
    public Inventory inv;
    public AudioSource miningSound;

    private void Start()
    {
        inv = GameObject.Find("MapGeneration").GetComponent<MapGeneration>().GetInventory().GetComponent<Inventory>();
        resources.Add(Resources.Load("Items/OreIronium") as GameObject);
        resources.Add(Resources.Load("Items/OreZonium") as GameObject);
        resources.Add(Resources.Load("Items/PowderInstabilium") as GameObject);
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
        if (collider.GetComponent<PlayerControl>() != null && collider.GetComponent<GunSwitcher>().isMining)
        {
            if (Input.GetMouseButton(0))
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
        if (collider.GetComponent<PlayerControl>() != null && collider.GetComponent<GunSwitcher>().isMining)
        {
            mining = false;
        }
    }

    void MineOre()
    {
        if (!mining)
        {
            CancelInvoke("ReduceOre");
            miningSound.Stop();
            return;
        }
        if (!IsInvoking())
        {
            InvokeRepeating("ReduceOre", 0, 1f);
            miningSound.Play(0);
            miningSound.loop = true;
        }
        return;
    }

    void ReduceOre()
    {

        for (int i = 0; i < miningSpeed; i++)
        {
            if (AddResourcesToInv(oreType))
                oreCapacity--;
        }

        Debug.Log(oreCapacity);
    }

    bool AddResourcesToInv(Ore oreType)
    {

        bool success = true;
        switch (oreType)
        {
            case Ore.Ironium:
                if (!inv.AddItem(resources[0]))
                    success = false;
                break;
            case Ore.Zonium:
                if (!inv.AddItem(resources[1]))
                    success = false;
                break;
            case Ore.Instabilium:
                if(!inv.AddItem(resources[2]))
                    success = false;
                break;
            default:
                Debug.Log("No resource found");
                success = false;
                break;
        }

        return success;
    }
}
