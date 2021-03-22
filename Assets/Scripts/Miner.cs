using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour {
    [SerializeField] private GameObject ironiumPrefab;
    [SerializeField] private GameObject zoniumPrefab;
    [SerializeField] private GameObject ventiumPrefab;
    [SerializeField] private GameObject memiumPrefab;
    [SerializeField] private GameObject unobtaniumPrefab;
    [SerializeField] private GameObject instabiliumPrefab;

    private Ore oreType;
    private GameObject prefab;

    public Transform spawnLocation;
    private Transform spawnParent;

    public float miningSpeed;
    public int amountMined;

    // optimize pls
    private void Start() {
        spawnParent = GameObject.Find("OresMined").transform;
    }

    public void setOre(Ore ore) {
        oreType = ore;
        switch (oreType) {
            case Ore.Ironium:
                prefab = ironiumPrefab;
                break;
            case Ore.Zonium:
                prefab = zoniumPrefab;
                break;
            case Ore.Ventium:
                prefab = ventiumPrefab;
                break;
            case Ore.Memium:
                prefab = memiumPrefab;
                break;
            case Ore.Unobtanium:
                prefab = unobtaniumPrefab;
                break;
            case Ore.Instabilium:
                prefab = instabiliumPrefab;
                break;
        }

        startMining();
    }

    public void startMining() {
        InvokeRepeating("spawnOre", 0f, miningSpeed);
    }

    private void spawnOre() {
        for(int i=0; i<amountMined; i++) {
            Debug.Log("Mine");
            Instantiate(prefab, spawnLocation.position, spawnLocation.rotation, spawnParent);
        }
    }
}
